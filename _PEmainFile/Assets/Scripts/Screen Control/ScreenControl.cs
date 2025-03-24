using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScreenControl : MonoBehaviour
{
    public GameObject screen0; //Start Screen
    public GameObject screen1; //Instructions Screen
    public GameObject screen2; //Tap to begin screen (placeholder)
    public GameObject screen3; //Photo Capture
    public GameObject screen4; //Preview GIF Screen
    public GameObject screen5; //Info Screen
    public GameObject screen6; //Confirmation Screen
    public GameObject screen7; //Success Screen

    public GameObject websosa; //webcam

    public GameObject flash;  //camera flash

    public GifGen giffy;
    public bool flashOn = false;

    public float flashTime = 0.3f;
    public Button resetButton;
    public GameObject BlockCLogo;
    public TransitionOverlay transitionOverlay; //fade to black

    public GameObject gifPrev; //preview gif screen :)

    public SpriteAnimator loader; //loads gif to quasigif

    void Start()
    {
            screen0.SetActive(true);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(false);
            flash.SetActive(false);
            gifPrev.SetActive(false);
       // Showscreen0(); //starts only showing screen0
    }

    void Update()
    {

    if(flashOn){
        flashTime -= Time.deltaTime;
        Flash();
    }

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

    public void Flash(){
        Debug.Log("in the cut");
        flashOn = true;
        flash.SetActive(true);
        if(flashTime < 0.0f){
            
            flashOn = false;
            flash.SetActive(false);
            flashTime = 0.3f;
        }

    }


    public void Showscreen0(){//Start Screen
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(true);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(false);
            flash.SetActive(false);
            gifPrev.SetActive(false);
        });
        Debug.Log("Start screen is active!");
    }

    public void Showscreen1(){//Instructions Screen
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(false);
            screen1.SetActive(true);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(false);
        });
        Debug.Log("instructions screen is active!");
    }

    public void ShowScreen2(){//Tap to begin screen
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(true);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(true);
    });
        Debug.Log("tap to begin screen is active!");
    }

    public void ShowScreen3(){//Photo Capture
        //transitionOverlay.FadeTransition(() => {
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(true);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(true);
            gifPrev.SetActive(false);
        //will need to make the photo capture sequence begin automatically
    //});
        Debug.Log("Photo Capture is active!");
    }

    public void ShowScreen4(){//Preview GIF Screen
        
        giffy.ConvertImagesToGif();
                    loader.LoadSprites();
            
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(true);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(false);
            gifPrev.SetActive(true);
           // giffy.Start();

            
        });
        Debug.Log("Preview GIF screen is active!");
    }

    public void ShowScreen5(){//Info Screen
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(true);
            screen6.SetActive(false);
            screen7.SetActive(false);
            gifPrev.SetActive(false);
        });
        Debug.Log("Info screen is active!");
    }

    public void ShowScreen6(){//Confirmation Screen
        transitionOverlay.FadeTransition(() => {
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

