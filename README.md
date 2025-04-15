

> A touchscreen-enabled Unity app that captures event photos, creates animated GIFs, and emails them to users. Built by CofC student devs for real-world events. 💛

  

---

  

## 🧭 Overview

  

This interactive photo booth app runs as a standalone Windows `.exe`. It guides users through a 4-photo sequence, builds an animated GIF, previews it on screen, and sends the final GIF via email. Everything is managed through a friendly on-screen UI — no keyboard or mouse required.

  

---

  

## 🖥️ Requirements

  

- A **Windows 10/11 PC**

- A **webcam**

- **Touchscreen** monitor (or mouse for testing)

- Internet access (for sending email)

- Unity 2022.x or newer if editing the project

  

---

  

## 📂 File Storage

  

The app writes temporary images and GIF files to your local filesystem.

  

| File Type | Location | Notes |

|-----------|----------|-------|

| Photos | `%AppData%/../LocalLow/CofCBooth/Photos/` | 4 photos per session |

| GIF | `%AppData%/../LocalLow/CofCBooth/gif/rad.gif` | Final animated output |

| Frames | `Assets/Frames/` | Custom PNG overlays used in GIF |

| Logo | `Assets/Logos/CofC.png` | Embedded in the email HTML |

  

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

  

| Element | How |

|--------|-----|

| Logo in emails | Replace `Assets/Logos/CofC.png` |

| Frame overlays | Add/remove files in `Assets/Frames/` |

| UI text | Edit TextMeshPro fields in Unity |

| Loading bar visuals | Replace sprites or adjust `LoadingBarScript.cs` |

| Email message | Edit `EmailController > EmailTemplate.GetHtmlBody()` |

  

---

  

## 🚀 How to Run

  

### Option 1 – From Unity (for devs)

  

1. Open the project in Unity 2022+

2. Open the `Start_Instructions` scene

3. Hit Play to simulate a full flow

  

### Option 2 – As `.exe` (for events)

  

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

- Folder paths are managed by `PathGetter.cs`

- Email sending runs on a background thread

  

---

  

## 👤 Author & Credits

  

- **Developers**: [Your Team Name]

- **Client**: College of Charleston

- **Built with**: Unity 2D, C#, Gmail API

  

Questions or feedback? Reach out at [your contact email].