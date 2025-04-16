# 📸 CofC Photo Booth

> A touchscreen-enabled Unity app that captures event photos, creates animated GIFs, and emails them to users. Built by CofC student developers for real-world events. 

---

## 🧭 Overview

This interactive photo booth app runs as a standalone Windows `.exe`. It guides users through a 4-photo sequence, builds an animated GIF, previews it on screen, and sends the final GIF via email. Everything is managed through a friendly on-screen UI — no keyboard or mouse required.

---

## 🖥️ Requirements

- A **Microsoft Surface**
- **Windows 10/11**
- Internet access (for sending email)
- Unity Editor Version 6000.0.38f1 if editing the project

---

## 📂 File Storage

The app writes temporary images and GIF files to your local filesystem.

| File Type | Location                                                         | Notes                      |
| --------- | ---------------------------------------------------------------- | -------------------------- |
| Photos    | `%AppData%/../LocalLow/CSCI495-COFC/Cofc-Photobooth/Photos/`     | 4 photos per session       |
| GIF       | `%AppData%/../LocalLow/CSCI495-COFC/Cofc-Photobooth/gif/rad.gif` | Final animated output      |
| Logo      | `StreamingAssets/CofC.png`                                       | Embedded in the email HTML |

**⚠️ Reset deletes all saved photos and GIFs.**

---

## ✉️ Email Functionality

- Uses Gmail SMTP (`smtp.gmail.com`)
- Sends `rad.gif` as an attachment
- Formats email with **HTML branding**
- Fallback to plain text body if HTML fails
- Built-in loading screen during sending

---

## 🧑‍🎨 What You Can Customize

Your team can update the following content without editing code:

| Element             | How                                                                             |
| ------------------- | ------------------------------------------------------------------------------- |
| Logo in emails      | Replace `StreamingAssets/CofC.png`                                              |
| Frame overlays      | Add/remove files in `%AppData%/../LocalLow/CSCI495-COFC/Cofc-Photobooth/Frames` |
| UI text             | Edit TextMeshPro fields in Unity                                                |
| Loading bar visuals | Replace sprites or adjust `LoadingBarScript.cs`                                 |
| Email message       | Edit `EmailController > EmailTemplate.GetHtmlBody()`                            |

---

## 🚀 How to Run

### Option 1 – From Unity (for devs)

1. Open the project in Unity Editor
2. Open the `MAINSCENE` scene
3. Hit Play to simulate a full flow

### Option 2 – As `.exe` (for events)??? TBD

1. Build the project via **File > Build Settings > Windows**
2. Transfer the `.exe` to your event laptop
3. Double-click to launch (fullscreen)
4. Tap to begin

---

## 🛠️ Setup Notes

- **App password** is required for Gmail SMTP
- No external Python scripts required
- Uses Unity coroutines and threads to prevent UI freezing
- Loading screens use `ScreenControl.RunWithLoadingScreen(...)`

---

## ⚠️ Known Issues

| Problem | Fix |
|--------|------|
| GIF preview shows blank | Delay screen load until `LoadSprites()` completes |
| Reset throws error after email | Make sure email finishes sending before reset |
| Loading screen freezes near end | Normal — email is blocking, handled by thread |
| Touch selects wrong button on screen change | Use `EventSystem.current.SetSelectedGameObject(null)` |

---

## 🧪 Developer Notes

- All screens controlled by `ScreenControl.cs`
- Screens are turned on/off using `ShowScreenX()` methods
- Image capture via `Webcam.cs`
- GIF generation via `GifGen.cs`
- GIF Preview simulation via `Animator.cs`
- Folder paths are managed by `PathGetter.cs`
- Email functionality via `ToggleEmail.cs`
- Email sending runs on a background thread

---

## 👤 Author & Credits

- **Developers**: [Briana Addison, Sophia Butcher, William Holmes, Justin Luft, Brett Widholm]
- **Client**: College of Charleston Department of Computer Science
- **Built with**: Unity 2D, C#, SMTP API, FFmpeg

Questions or feedback? Reach out at [your contact email].
