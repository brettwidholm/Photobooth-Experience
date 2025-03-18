using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScreenControl : MonoBehaviour
{
    public GameObject devMode; //dev mode 
    public GameObject screen0; //Start Screen
    public GameObject screen1; //Instructions Screen
    public GameObject screen2; //Tap to begin screen (placeholder)
    public GameObject screen3; //Photo Capture
    public GameObject screen4; //Preview GIF Screen
    public GameObject screen5; //Info Screen
    public GameObject screen6; //Confirmation Screen
    public GameObject screen7; //Success Screen
    public Button resetButton;
    public GameObject BlockCLogo;
    public TransitionOverlay transitionOverlay; //fade to black

    void Start()
    {
        //Showscreen0(); //starts only showing screen0 but does fade transition on start
        screen0.SetActive(true); // start with start screen active (here just in case it gets disabled in hierarchy)
    }

    void Update()
    {
    if(IsScreenActive("Start Screen")){
        resetButton.gameObject.SetActive(false);
     }
     else{
        resetButton.gameObject.SetActive(true);
     }

     if(IsScreenActive("Tap to Begin Screen")){
            if (Input.GetMouseButtonDown(0)){
                Debug.Log("we going to the next screen");
                ShowScreen3(); //go to photo capture screen
            }
     }
    }
    public void ShowDevMode(){//Dev Mode
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(true);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
        });
        Debug.Log("Dev mode is active!");
    }
    public void Showscreen0(){//Start Screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(true);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
        });
        Debug.Log("Start screen is active!");
    }

    public void Showscreen1(){//Instructions Screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(true);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
        });
        Debug.Log("instructions screen is active!");
    }

    public void ShowScreen2(){//Tap to begin screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(true);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
    });
        Debug.Log("tap to begin screen is active!");
    }

    public void ShowScreen3(){//Photo Capture
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(true);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
        //will need to make the photo capture sequence begin automatically
    });
        Debug.Log("Photo Capture is active!");
    }

    public void ShowScreen4(){//Preview GIF Screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(true);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
        });
        Debug.Log("Preview GIF screen is active!");
    }

    public void ShowScreen5(){//Info Screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(true);
            screen6.SetActive(false);
            screen7.SetActive(false);
        });
        Debug.Log("Info screen is active!");
    }

    public void ShowScreen6(){//Confirmation Screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(true);
            screen7.SetActive(false);
        });
        Debug.Log("Confirmation screen is active!");
    }
        public void ShowScreen7(){//Success Screen
        transitionOverlay.FadeTransition(() => {
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(true);
        });
        Debug.Log("Success screen is active!");
    }

    public bool IsScreenActive(string screenName){
        GameObject screen = GameObject.Find(screenName);
        if (screen != null){
            return screen.activeInHierarchy;
        }
        else{
            return false;
        }
    }

}

