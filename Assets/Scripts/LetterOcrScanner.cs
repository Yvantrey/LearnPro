using System;
using System.Text.RegularExpressions;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Reads a large, single Latin letter from the AR camera with the Android ML Kit plugin.
/// The native plugin is deliberately called only a few times per second to keep the
/// camera preview responsive.
/// </summary>
public class LetterOcrScanner : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject letterPanelPrefab;
    [SerializeField] private Canvas overlayCanvas;

    [Header("Scanning")]
    [SerializeField, Min(0.25f)] private float scanInterval = 0.7f;
    [SerializeField, Range(40, 100)] private int jpegQuality = 80;
    [SerializeField, Min(256)] private int maximumImageDimension = 960;

    private ARCameraManager cameraManager;
    private GameObject activePanel;
    private bool nativeRequestInFlight;
    private float nextScanTime;
    private string displayedLetter;

    private void Awake()
    {
        cameraManager = FindFirstObjectByType<ARCameraManager>();

        if (overlayCanvas == null)
        {
            Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            foreach (Canvas canvas in canvases)
            {
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    overlayCanvas = canvas;
                    break;
                }
            }
        }
    }

    private void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (cameraManager == null || nativeRequestInFlight || Time.unscaledTime < nextScanTime)
            return;

        ScanLatestCameraImage();
#endif
    }

    private void ScanLatestCameraImage()
    {
        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            return;

        nextScanTime = Time.unscaledTime + scanInterval;

        using (image)
        {
            float scale = Mathf.Min(1f, maximumImageDimension / (float)Mathf.Max(image.width, image.height));
            int width = Mathf.Max(1, Mathf.RoundToInt(image.width * scale));
            int height = Mathf.Max(1, Mathf.RoundToInt(image.height * scale));

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(width, height),
                outputFormat = TextureFormat.RGBA32,
                transformation = XRCpuImage.Transformation.MirrorY
            };

            int dataSize = image.GetConvertedDataSize(conversionParams);
            var imageData = new NativeArray<byte>(dataSize, Allocator.Temp);
            image.Convert(conversionParams, imageData);

            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.LoadRawTextureData(imageData);
            texture.Apply(false, false);
            byte[] jpegBytes = texture.EncodeToJPG(jpegQuality);

            Destroy(texture);
            imageData.Dispose();

            nativeRequestInFlight = true;
            try
            {
                using (var recognizer = new AndroidJavaClass("com.learnpro.letterocr.LetterTextRecognizer"))
                {
                    recognizer.CallStatic("recognizeJpeg", jpegBytes, gameObject.name);
                }
            }
            catch (Exception exception)
            {
                nativeRequestInFlight = false;
                Debug.LogError($"Letter OCR could not start: {exception.Message}");
            }
        }
    }

    // Called by the Android ML Kit plugin through UnitySendMessage.
    public void OnOcrResult(string detectedText)
    {
        nativeRequestInFlight = false;

        Match match = Regex.Match(detectedText ?? string.Empty, @"^[A-Za-z]$");
        if (!match.Success)
            return;

        ShowWords(match.Value.ToUpperInvariant());
    }

    // Called by the Android ML Kit plugin if it cannot process a camera frame.
    public void OnOcrFailure(string error)
    {
        nativeRequestInFlight = false;
        Debug.LogWarning($"Letter OCR: {error}");
    }

    private void ShowWords(string letter)
    {
        if (letter == displayedLetter)
            return;

        if (letterPanelPrefab == null || overlayCanvas == null)
        {
            Debug.LogError("Letter OCR needs a Letter Panel prefab and a screen-space Canvas.");
            return;
        }

        if (activePanel != null)
            Destroy(activePanel);

        activePanel = Instantiate(letterPanelPrefab, overlayCanvas.transform, false);
        Canvas panelCanvas = activePanel.GetComponent<Canvas>();
        if (panelCanvas != null)
        {
            panelCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            panelCanvas.worldCamera = null;
        }

        RectTransform panelRect = activePanel.GetComponent<RectTransform>();
        if (panelRect != null)
        {
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.anchoredPosition = Vector2.zero;
            panelRect.localScale = Vector3.one;
        }

        LetterPanel panel = activePanel.GetComponent<LetterPanel>();
        if (panel == null)
        {
            Debug.LogError("Letter OCR: the Letter Panel prefab has no LetterPanel component.");
            return;
        }

        panel.ShowLetter(letter);
        displayedLetter = letter;
    }
}
