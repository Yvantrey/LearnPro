using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AlphabetImageTracker : MonoBehaviour
{
    [Header("AR Tracking")]
    public ARTrackedImageManager trackedImageManager;

    [Header("AR Panel")]
    public GameObject letterPanelPrefab;

    [Header("Panel Position")]
    [Tooltip("Distance in front of the detected letter image.")]
    public Vector3 panelOffset = new Vector3(0f, 0f, 0.01f);

    [Tooltip("World-space scale for the word panel.")]
    public float panelScale = 0.001f;

    private Dictionary<TrackableId, GameObject> panels =
        new Dictionary<TrackableId, GameObject>();

    private void OnEnable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
        }
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
        }
    }

    private void OnTrackedImagesChanged(
        ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            CreatePanel(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdatePanel(trackedImage);
        }

        foreach (KeyValuePair<TrackableId, ARTrackedImage> pair in eventArgs.removed)
        {
            RemovePanel(pair.Value);
        }
    }

    private void CreatePanel(ARTrackedImage trackedImage)
    {
        if (letterPanelPrefab == null)
        {
            Debug.LogError("AlphabetImageTracker: Letter Panel Prefab is missing.");
            return;
        }

        if (panels.ContainsKey(trackedImage.trackableId))
            return;

        GameObject panel = Instantiate(
            letterPanelPrefab,
            trackedImage.transform
        );

        SetPanelTransform(panel);

        LetterPanel letterPanel = panel.GetComponent<LetterPanel>();

        if (letterPanel != null)
        {
            string letter = trackedImage.referenceImage.name;
            letterPanel.ShowLetter(letter);
        }
        else
        {
            Debug.LogError(
                "AlphabetImageTracker: LetterPanel prefab does not contain LetterPanel.cs."
            );
        }

        panels.Add(trackedImage.trackableId, panel);

        UpdatePanel(trackedImage);
    }

    private void UpdatePanel(ARTrackedImage trackedImage)
    {
        if (!panels.TryGetValue(
                trackedImage.trackableId,
                out GameObject panel))
        {
            return;
        }

        bool isTracking =
            trackedImage.trackingState == TrackingState.Tracking;

        panel.SetActive(isTracking);

        if (isTracking)
        {
            SetPanelTransform(panel);

            LetterPanel letterPanel =
                panel.GetComponent<LetterPanel>();

            if (letterPanel != null)
            {
                string letter = trackedImage.referenceImage.name;
                letterPanel.ShowLetter(letter);
            }
        }
    }

    private void RemovePanel(ARTrackedImage trackedImage)
    {
        if (panels.TryGetValue(
                trackedImage.trackableId,
                out GameObject panel))
        {
            Destroy(panel);
            panels.Remove(trackedImage.trackableId);
        }
    }

    private void SetPanelTransform(GameObject panel)
    {
        panel.transform.localPosition = panelOffset;
        panel.transform.localRotation = Quaternion.identity;
        panel.transform.localScale = Vector3.one * panelScale;

        if (panel.TryGetComponent(out RectTransform panelRect))
        {
            panelRect.anchoredPosition = Vector2.zero;
        }
    }
}
