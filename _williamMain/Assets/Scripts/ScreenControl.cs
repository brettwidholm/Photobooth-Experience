using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    //references to screens
    public GameObject screen0; //Start Screen
    public GameObject screen1; //Instructions Screen
    public GameObject screen2; //Tap to begin screen (placeholder)
    public TransitionOverlay transitionOverlay; //fade to black

    void Awake() // Runs as soon as the scene loads (before Play Mode)
    {
        SetInitialScreen();
    }
    void Start()
    {
        //starts only showing screen0
        Showscreen0();
    }

    
        void SetInitialScreen()
    {
        if (screen0 != null && screen1 != null)
        {
            screen0.SetActive(true);
            screen1.SetActive(false);
            screen2.SetActive(false);
        }
    }

    // Runs when any value in the Inspector changes 
    // to make sure the first screen is the actual screen shown in the scene view and game view frame before playing
    //I think this needs fixing because there is an immediate fade to black when playing
    void OnValidate(){
        if (!Application.isPlaying){
            SetInitialScreen();
        }
    }
    public void Showscreen0(){
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(true);
            screen1.SetActive(false);
            screen2.SetActive(false);
            
        });
        Debug.Log("Start screen is active!");
    }

    public void Showscreen1(){
        transitionOverlay.FadeTransition(() => {
            screen0.SetActive(false);
            screen1.SetActive(true);
            screen2.SetActive(false);
            
        });
        Debug.Log("instructions screen is active!");
    }

    public void ShowScreen2(){
        transitionOverlay.FadeTransition(() => {
        screen0.SetActive(false);
        screen1.SetActive(false);
        screen2.SetActive(true);
        
    });
    Debug.Log("tap to begin screen is active!");
}

public bool IsScreenActive(string screenName){
    GameObject screen = GameObject.Find(screenName);
    if (screen != null){
        return screen.activeInHierarchy;
    }
    else{
        //Debug.LogWarning($"Screen {screenName} not found!");
        return false;
    }
}

}

