package com.learnpro.letterocr;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.mlkit.vision.common.InputImage;
import com.google.mlkit.vision.text.Text;
import com.google.mlkit.vision.text.TextRecognition;
import com.google.mlkit.vision.text.latin.TextRecognizerOptions;
import com.google.mlkit.vision.text.TextRecognizer;

import java.lang.reflect.Method;
import java.util.Locale;

/** Android bridge for the Unity LetterOcrScanner component. */
public final class LetterTextRecognizer {
    private static TextRecognizer recognizer;

    private LetterTextRecognizer() { }

    public static void recognizeJpeg(byte[] jpegBytes, final String receiverObjectName) {
        if (jpegBytes == null || jpegBytes.length == 0) {
            send(receiverObjectName, "OnOcrFailure", "The camera frame was empty.");
            return;
        }

        Bitmap bitmap = BitmapFactory.decodeByteArray(jpegBytes, 0, jpegBytes.length);
        if (bitmap == null) {
            send(receiverObjectName, "OnOcrFailure", "The camera frame could not be decoded.");
            return;
        }

        if (recognizer == null) {
            recognizer = TextRecognition.getClient(TextRecognizerOptions.DEFAULT_OPTIONS);
        }

        InputImage image = InputImage.fromBitmap(bitmap, 0);
        recognizer.process(image)
            .addOnSuccessListener(new OnSuccessListener<Text>() {
                @Override
                public void onSuccess(Text text) {
                    send(receiverObjectName, "OnOcrResult", findLargestSingleLetter(text));
                }
            })
            .addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(Exception exception) {
                    send(receiverObjectName, "OnOcrFailure", exception.getMessage());
                }
            });
    }

    private static String findLargestSingleLetter(Text text) {
        String bestLetter = "";
        int largestArea = 0;

        for (Text.TextBlock block : text.getTextBlocks()) {
            for (Text.Line line : block.getLines()) {
                String candidate = line.getText().trim().toUpperCase(Locale.US);
                Rect bounds = line.getBoundingBox();
                int area = bounds == null ? 0 : bounds.width() * bounds.height();

                if (candidate.matches("[A-Z]") && area > largestArea) {
                    bestLetter = candidate;
                    largestArea = area;
                }
            }
        }

        return bestLetter;
    }

    private static void send(String receiverObjectName, String methodName, String value) {
        try {
            Class<?> unityPlayer = Class.forName("com.unity3d.player.UnityPlayer");
            Method unitySendMessage = unityPlayer.getMethod(
                "UnitySendMessage", String.class, String.class, String.class);
            unitySendMessage.invoke(null, receiverObjectName, methodName, value == null ? "" : value);
        } catch (Exception ignored) {
            // Unity is not available only when this library is invoked outside the player.
        }
    }
}
