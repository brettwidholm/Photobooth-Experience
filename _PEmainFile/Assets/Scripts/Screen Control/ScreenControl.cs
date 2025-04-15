using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using System;
using System.Collections;

public class ScreenControl : MonoBehaviour
{

    //screens/transitions:
    //-------------------------------------
    public GameObject devMode; //dev mode screen
    public GameObject screen0; //Start Screen
    public GameObject screen1; //Instructions Screen
    public GameObject screen2; //Tap to begin screen (placeholder)
    public GameObject screen3; //Photo Capture
    public GameObject screen4; //Preview GIF Screen
    public GameObject screen5; //Info Screen
    public GameObject screen6; //Confirmation Screen
    public GameObject screen7; //Success Screen
    public GameObject loadingScreen; //Loading screen
    public LoadingBarScript loadingBar;

    public TransitionOverlay transitionOverlay; //fade to white
    
    
    //-------------------------------------------------
    //webcam related:
    //-------------------------------------------------
    public GameObject websosa; //webcam
    public Webcam webcamScript;
    public GameObject flash;  //camera flash
    public bool flashOn = false;
    public float flashTime = 0.3f;

    //-----------------------------------------------
    //gif related:
    //------------------------------------------------
    public GifGen giffy; //object for generating the gif itself
     public GameObject gifPrev; //preview gif screen :)
     public SpriteAnimator loader; //loads gif to quasigif (takes images in folder and makes animation)
    private Vector3 originalGifPrevScale;

    //-----------------------------------------
    //buttons and logos:
    //-----------------------------------
    public GameObject BlockCLogo; //our lovely cofc logo
    public Button resetButton;  //global reset button

    public GameObject backButtonInstructions; //back button on instructions
    public GameObject backButtonInfo; //back button on info

    public GameObject backButtonConfirm; //back button on confirmation

    public GameObject previewShareButton; //share button on preview

    public GameObject nextButtonInfo; //next button on info

    public GameObject sendEmailButton; //send button for email
    
    public GameObject emailEntryBox; //the box to enter email

    void Start()
    {
        devMode.SetActive(false);
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
        loadingScreen.SetActive(false);
        
        
        backButtonInstructions.SetActive(false); //sets all necessary button to be turned off
        backButtonInfo.SetActive(false);
        backButtonConfirm.SetActive(false);
        previewShareButton.SetActive(false);
        nextButtonInfo.SetActive(false);
        sendEmailButton.SetActive(false);
        emailEntryBox.SetActive(false);
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

/*     public void Flash(){
        //Debug.Log("in the cut");
        flashOn = true;
        flash.SetActive(true);
        if(flashTime < 0.0f){
            flashOn = false;
            flash.SetActive(false);
            flashTime = 0.3f;
        }
    } */
    public void Flash()
{
    StartCoroutine(FlashRoutine());
}

private IEnumerator FlashRoutine()
{
    flash.SetActive(true);
    yield return new WaitForSeconds(flashTime);
    flash.SetActive(false);
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
            websosa.SetActive(false);
            flash.SetActive(false);
            gifPrev.SetActive(false);
            emailEntryBox.SetActive(false);

            backButtonInstructions.SetActive(false); //sets all necessary button to be turned off
            backButtonInfo.SetActive(false);
            backButtonConfirm.SetActive(false);
            previewShareButton.SetActive(false);
            nextButtonInfo.SetActive(false);
            sendEmailButton.SetActive(false);
            emailEntryBox.SetActive(false);
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
            websosa.SetActive(false);

            backButtonInstructions.SetActive(true);
        });
        Debug.Log("instructions screen is active!");
    }

        public void ShowScreen2()
    {
            // Any setup logic before camera activation can go here
            Debug.Log("showscreen2() called");
            
            transitionOverlay.FadeTransition(() => {
                websosa.SetActive(true);
                
                devMode.SetActive(false);
                screen0.SetActive(false);
                screen1.SetActive(false);
                screen2.SetActive(true);

            loadingScreen.SetActive(false);
            Debug.Log("Loading screen deactivated");

                screen3.SetActive(false);
                screen4.SetActive(false);
                screen5.SetActive(false);
                screen6.SetActive(false);
                screen7.SetActive(false);

                backButtonInstructions.SetActive(false);
            });

            Debug.Log("tap to begin screen is active!");
        }
    

    public void ShowScreen3(){//Photo Capture
            devMode.SetActive(false);
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
            previewShareButton.SetActive(false);

        Debug.Log("Photo Capture is active!");
    }

    public void ShowScreen4(){//Preview GIF Screen
        
        /* giffy.ConvertImagesToGif();
        loader.LoadSprites(); */
            
        transitionOverlay.FadeTransition(() => {
            gifPrev.SetActive(true);
            devMode.SetActive(false);
            screen0.SetActive(false);
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(true);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);
            websosa.SetActive(false);
            

            loadingScreen.SetActive(false);
            Debug.Log("Loading screen deactivated");
            

            emailEntryBox.SetActive(false);
            previewShareButton.SetActive(true);
            backButtonInfo.SetActive(false);
            backButtonConfirm.SetActive(false);
            nextButtonInfo.SetActive(false);
           // giffy.Start();

            
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
            gifPrev.SetActive(false);

            emailEntryBox.SetActive(true);
            previewShareButton.SetActive(false);
            backButtonInfo.SetActive(true);
            sendEmailButton.SetActive(false);
            nextButtonInfo.SetActive(true);
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

            emailEntryBox.SetActive(false);
            backButtonConfirm.SetActive(true);
            sendEmailButton.SetActive(true);
            nextButtonInfo.SetActive(false);
            gifPrev.SetActive(true);
            
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
            loadingScreen.SetActive(false);

            gifPrev.SetActive(false);
            backButtonConfirm.SetActive(false);
            sendEmailButton.SetActive(false);
        });
        Debug.Log("Success screen is active!");
    }
public void showloadingScreen(Action onShown = null, bool useFade = true)
{
    Action activate = () =>
    {
        loadingBar?.ResetBar(); // ✅ Reset bar before it becomes visible

        loadingScreen.SetActive(true);

        devMode.SetActive(false);
        screen0.SetActive(false);
        screen1.SetActive(false);
        screen2.SetActive(false);
        screen3.SetActive(false);
        screen4.SetActive(false);
        screen5.SetActive(false);
        screen6.SetActive(false);
        screen7.SetActive(false);
        gifPrev.SetActive(false);
        backButtonConfirm.SetActive(false);
        sendEmailButton.SetActive(false);

        onShown?.Invoke();
    };

    if (useFade)
    {
        transitionOverlay.FadeTransition(activate);
    }
    else
    {
        activate.Invoke();
    }
}


/*     public void showloadingScreen(Action onShown = null)
{
    //loadingBar?.ResetBar(); // reset bar to 0 before screen is active

    transitionOverlay.FadeTransition(() =>
    {
        loadingScreen.SetActive(true);

        // Deactivate other screens
        devMode.SetActive(false);
        screen0.SetActive(false);
        screen1.SetActive(false);
        screen2.SetActive(false);
        screen3.SetActive(false);
        screen4.SetActive(false);
        screen5.SetActive(false);
        screen6.SetActive(false);
        screen7.SetActive(false);
        gifPrev.SetActive(false);
        backButtonConfirm.SetActive(false);
        sendEmailButton.SetActive(false);

        onShown?.Invoke(); // kick off the rest AFTER fade and loading screen is visible
    });
} */


    public bool IsScreenActive(string screenName){
        GameObject screen = GameObject.Find(screenName);
        if (screen != null){
            return screen.activeInHierarchy;
        }
        else{
            return false;
        }
    }

    public void TouchKeyboard(){
        System.Diagnostics.Process.Start("tabtip.exe");
    }

    private const uint WM_SYSCOMMAND = 0x0112;
    private const uint SC_CLOSE = 0xF060;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);

    public static void CloseTouchKeyboard()
    {
        IntPtr keyboardWnd = FindWindow("IPTip_Main_Window", null);
        if (keyboardWnd != IntPtr.Zero)
        {
            PostMessage(keyboardWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Touch keyboard window not found.");
        }
    }

    public void DeselectCurrentInput()
    {
        StartCoroutine(WaitAndDeselect());
    }

    private IEnumerator WaitAndDeselect()
    {
        yield return null; // Wait a frame for Unity to process the end-edit event

        var selected = EventSystem.current.currentSelectedGameObject;

        // Even if another field is selected, still deselect THIS one
        if (selected != null && selected.GetComponent<TMP_InputField>() != null)
        {
            // User is clicking into another field — don’t kill the keyboard
            yield break;
        }

        // User pressed Return or clicked away
        CloseTouchKeyboard();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void RunWithLoadingScreen(Action onComplete, Action onStart = null, float delay = 1f)
    {
        StartCoroutine(HandleWithLoadingScreen(onComplete, onStart, delay));
    }

    IEnumerator HandleWithLoadingScreen(Action actionAfter, Action onComplete = null, float delay = 1f)
    {
        bool screenReady = false;

        showloadingScreen(() => {
            screenReady = true;
        });

        yield return new WaitUntil(() => screenReady); // wait for fade to complete

        loadingBar?.IncreaseLoading(1f); // now it's safe to animate

        yield return new WaitForSeconds(delay); // let loading bar run

        //loadingBar?.CompleteLoading(); // force fill if needed
        //loadingScreen.SetActive(false);
        yield return new WaitForEndOfFrame();

        actionAfter?.Invoke();
        onComplete?.Invoke();
    }


        // Call this from Button in Inspector to go to Screen2 with a loading screen
    public void ShowScreen2_WithLoading()
    {
        webcamScript.StartWebcamFeed();
        RunWithLoadingScreen(() => ShowScreen2(), null, 3.0f);
    }

/*     public void ShowScreen4_WithLoading()
    {
        giffy.ConvertImagesToGif();
        loader.LoadSprites();
        RunWithLoadingScreen(() => ShowScreen4(), null, 3.0f);
    }
     */
}

