using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    //references to screens
    public GameObject screenA; //Start Screen
    public GameObject screenB; //Instructions Screen
    public GameObject beginScreen; //Tap to begin screen (placeholder)
    public TransitionOverlay transitionOverlay; //fade to black

    void Awake() // Runs as soon as the scene loads (before Play Mode)
    {
        SetInitialScreen();
    }
    void Start()
    {
        //starts only showing screenA
        ShowScreenA();
    }

    
        void SetInitialScreen()
    {
        if (screenA != null && screenB != null)
        {
            screenA.SetActive(true);
            screenB.SetActive(false);
            beginScreen.SetActive(false);
        }
    }

    // Runs when any value in the Inspector changes 
    // to make sure the first screen is the actual screen shown in the scene view and game view frame before playing
    //I think this needs fixing because there is an immediate fade to black when playing
    void OnValidate()
    {
        if (!Application.isPlaying) 
        {
            SetInitialScreen();
        }
    }
    public void ShowScreenA(){
        transitionOverlay.FadeTransition(() => {
            screenA.SetActive(true);
            screenB.SetActive(false);
            beginScreen.SetActive(false);
        });
            
    }

    public void ShowScreenB(){
        transitionOverlay.FadeTransition(() => {
            screenA.SetActive(false);
            screenB.SetActive(true);
            beginScreen.SetActive(false);
        });
    }

    public void ShowScreenC(){
        transitionOverlay.FadeTransition(() => {
        screenA.SetActive(false);
        screenB.SetActive(false);
        beginScreen.SetActive(true);
    });
}
}
