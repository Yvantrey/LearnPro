LearnPro – AR Alphabet Learning

About the Project

LearnPro is an educational AR mobile application designed to help children learn the alphabet in a fun and interactive way.

The app uses Augmented Reality (AR) and image tracking to recognize letters from A to Z. When a child points the phone's camera at a letter, the app recognizes it and displays simple example words that start with that letter.

For example, when the child scans A, the AR experience displays:

Apple

Ant

Airplane

Alligator

The words appear in AR above the scanned letter, making the learning experience more interactive than simply reading from a book.

Main Features

🔤 Learning letters from A–Z

📱 Mobile AR experience

📷 Camera-based letter recognition

🧒 Designed for children and beginner learners

📝 Displays simple words for each letter

✨ AR content appears above the scanned letter

🎮 Interactive and visual learning experience

Requirements

Unity with the LearnPro project

Android phone with ARCore support

USB cable for building/installing the app

Printed A–Z reference letters

Good lighting for image tracking

How to Run the Project

1. Open the Project

Open the LearnPro project in Unity.

2. Open the Main Scene

In the Project window, go to:

Assets → Scenes → MainScene

Open MainScene.

3. Check the AR Image Library

Select XR Origin in the Hierarchy.

Under AR Tracked Image Manager, make sure:

Serialized Library: AlphabetLibrary

Max Number Of Moving Images: 1

Tracked Image Prefab: None

4. Check the Alphabet Tracker

On XR Origin, make sure Alphabet Image Tracker is attached.

Check that:

Tracked Image Manager: AR Tracked Image Manager

Letter Panel Prefab: LetterPanel

5. Check the Alphabet Library

The AlphabetLibrary should contain the reference images for:

A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z

Each letter is used by AR Foundation to recognize the corresponding letter.

Running the App on an Android Phone

1. Connect the Phone

Connect the Android phone to the computer using a USB cable.

Make sure USB debugging is enabled on the phone.

2. Open Build Profiles

In Unity, go to:

File → Build Profiles

Select Android.

Make sure MainScene is included in the scenes list.

3. Build and Run

Select:

Build And Run

Unity will build the application and install it on the connected Android phone.

How a Child Uses the App

Open LearnPro.

Allow camera permission.

Place or hold one of the alphabet letters in front of the camera.

Point the camera at the letter.

Wait for AR Foundation to recognize the letter.

The app displays the letter and example words in AR.

Move to another letter to continue learning.

Example

If the child scans B, the app displays:

B

Ball
Banana
Bird
Book

If the child scans C, it displays:

C

Cat
Car
Cake
Cow

This allows the child to connect the letter, its appearance, and example words.

Project Scripts

AlphabetImageTracker.cs

This script handles the AR image tracking system. It detects which alphabet reference image is being recognized and creates or updates the AR learning panel.

LetterPanel.cs

This script controls the information displayed to the child. It receives the detected letter and displays the corresponding example words.

Troubleshooting

The camera is black

Make sure camera permission has been allowed.

Make sure the Android device supports ARCore.

Check the AR Foundation and Android ARCore configuration.

Restart the application after granting camera permission.

The letter is not detected

Make sure the correct letter is included in AlphabetLibrary.

Use good lighting.

Keep the complete letter visible.

Avoid glare and strong reflections.

Hold the phone steady.

Move the phone slightly closer or farther away.

Words do not appear

Check that:

AlphabetImageTracker is attached to the XR Origin.

AR Tracked Image Manager is assigned.

AlphabetLibrary is assigned to Serialized Library.

LetterPanel.prefab is assigned to Letter Panel Prefab.

LetterPanel.cs is attached to the LetterPanel prefab.

The prefab contains LetterText and WordsText.

Educational Purpose

LearnPro is designed to make early alphabet learning interactive, visual, and engaging for children.

Instead of only looking at letters in a traditional book, children can use their phone to scan letters and see learning information appear directly in the real world through AR.

The project demonstrates how AR technology can be used as an educational tool to support children's early learning and letter recognition.

Technology Used

Unity

C#

AR Foundation

ARCore

Image Tracking

TextMeshPro

Android

Project Goal

The goal of LearnPro is to create a simple and enjoyable AR learning experience where children can explore the alphabet from A to Z and learn example words associated with each letter.