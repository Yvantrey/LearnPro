# 📚 LearnPro – AR Alphabet Learning

**LearnPro** is an educational AR mobile application designed to help children learn the alphabet in a fun and interactive way.

The app uses **Augmented Reality (AR)** and **image tracking** to recognize letters from **A to Z**. When a child points the phone's camera at a letter, the app recognizes it and displays simple example words that start with that letter.

> For example, when the child scans **A**, the AR experience displays: **Apple**, **Ant**, **Airplane**, **Alligator** — appearing in AR above the scanned letter.

---

## ✨ Features

- 🔤 Learn all letters from **A–Z**
- 📱 Mobile **AR experience**
- 📷 **Camera-based** letter recognition
- 🧒 Designed for **children** and beginner learners
- 📝 Displays **4 example words** per letter
- ✨ AR content appears **above the scanned letter**
- 🎮 Interactive and **visual learning** experience

---

## 🛠️ Requirements

| Requirement | Details |
|---|---|
| **Unity** | With the LearnPro project open |
| **Android Phone** | With ARCore support |
| **USB Cable** | For building and installing the app |
| **Printed Letters** | A–Z reference cards |
| **Lighting** | Good lighting for image tracking |

---

## 🚀 How to Run the Project

### 1. Open the Project
Open the **LearnPro** project in Unity.

### 2. Open the Main Scene
In the Project window, navigate to:
```
Assets → Scenes → MainScene
```

### 3. Check the AR Image Library
Select **XR Origin** in the Hierarchy. Under **AR Tracked Image Manager**, make sure:
- **Serialized Library:** `AlphabetLibrary`
- **Max Number Of Moving Images:** `1`
- **Tracked Image Prefab:** `None`

### 4. Check the Alphabet Tracker
On **XR Origin**, make sure **Alphabet Image Tracker** is attached with:
- **Tracked Image Manager:** `AR Tracked Image Manager`
- **Letter Panel Prefab:** `LetterPanel`

### 5. Check the Alphabet Library
**AlphabetLibrary** should contain reference images for all 26 letters:
```
A B C D E F G H I J K L M N O P Q R S T U V W X Y Z
```

---

## 📱 Build & Run on Android

### 1. Connect the Phone
- Connect your Android phone via **USB cable**
- Enable **USB Debugging** on the phone

### 2. Open Build Profiles
In Unity, go to:
```
File → Build Profiles → Android
```
Make sure **MainScene** is included in the scenes list.

### 3. Build and Run
Click **Build And Run** — Unity will build and install the app on your phone.

---

## 🧒 How a Child Uses the App

1. Open **LearnPro**
2. Allow **camera permission**
3. Hold one of the **printed alphabet letters** in front of the camera
4. Wait for **AR Foundation** to recognize the letter
5. The app displays the **letter and example words** in AR
6. Move to another letter to **continue learning**

---

## 💡 Example

| Scanned Letter | Words Displayed |
|---|---|
| **B** | Ball, Banana, Bird, Book |
| **C** | Cat, Car, Cake, Cow |
| **D** | Dog, Duck, Drum, Dolphin |

---

## 📜 Project Scripts

### `AlphabetImageTracker.cs`
Handles the **AR image tracking system**. Detects which alphabet reference image is being recognized and creates or updates the AR learning panel.

### `LetterPanel.cs`
Controls the **information displayed** to the child. Receives the detected letter and displays the corresponding example words using TextMeshPro.

---

## 🔧 Troubleshooting

### 📷 The camera is black
- Make sure **camera permission** has been allowed
- Make sure the device supports **ARCore**
- Check **AR Foundation** and **ARCore** configuration
- Restart the app after granting permission

### 🔤 The letter is not detected
- Make sure the letter is included in **AlphabetLibrary**
- Use **good lighting** and avoid glare
- Keep the **full letter visible** in the frame
- Hold the phone **steady**
- Try moving **closer or farther** from the letter

### 📝 Words do not appear
Make sure all of the following are correctly set up:
- `AlphabetImageTracker` is attached to **XR Origin**
- `AR Tracked Image Manager` is assigned
- `AlphabetLibrary` is assigned to **Serialized Library**
- `LetterPanel.prefab` is assigned to **Letter Panel Prefab**
- `LetterPanel.cs` is attached to the **LetterPanel prefab**
- The prefab contains **LetterText** and **WordsText**

---

## 🧰 Technology Used

| Technology | Purpose |
|---|---|
| **Unity** | Game engine |
| **C#** | Scripting language |
| **AR Foundation** | Cross-platform AR framework |
| **ARCore** | Android AR support |
| **Image Tracking** | Letter recognition |
| **TextMeshPro** | UI text rendering |
| **Android** | Target platform |

---

## 🎯 Project Goal

The goal of **LearnPro** is to create a simple and enjoyable AR learning experience where children can explore the alphabet from **A to Z** and learn example words associated with each letter — making early education more **interactive, visual, and engaging**.
