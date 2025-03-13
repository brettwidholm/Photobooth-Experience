/*
using UnityEngine;
using System.Collections;
using TMPro;

//thank you https://discussions.unity.com/t/simple-timer/56201/2 :)

public class Timer: MonoBehaviour {

public float programTime = 20.0f;
public TextMeshProUGUI timerText;

public TextMeshProUGUI warningText; 

public void Start(){
    Debug.Log("GO GO GO!!!!");
    timerText.text = $"Timer: {programTime:F0}"; //no decimals, that creates more panic than needed
    warningText.text = "TOUCH THE SCREEN";
    timerText.enabled = false;
    warningText.enabled = false;
    //warning.HideWarning();
    Update();
}

public void Update(){
    
Debug.Log("updating timer");
//Console.WriteLine("updating timer");

timerText.text = $"Timer: {programTime:F0}";

if (Input.GetMouseButtonDown(0)){
    programTime = 20.0f; //if left click made reset timer 

    //in this branch we can add a circle that pops up to show a click (might be nice)
}


if(State.GetState() != 0 && State.GetState() != 3  ){

    programTime -= Time.deltaTime;
}
else if(State.GetState() == 0 || State.GetState() == 3) {
    programTime = 20.0f;

}

if (programTime <= 0.0f)
{
   Debug.Log("out of time... bye :(");
   //Console.WriteLine("out of time... bye :(");
   State.StateReset();
   //TimerPause();
}

if (programTime <= 5.0f){
    
    timerText.enabled = true;
    warningText.enabled = true;
   // warning.ShowWarning();
}
else if(programTime > 5.0f){
    
    timerText.enabled = false;  
    warningText.enabled = false;
   // warning.HideWarning(); 
}
}


}
*/


using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float programTime = 20.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI warningText;

    public ScreenControl screenControl; // Reference to ScreenControl

    private Transform currentScreen; // The active screen (panel)

    void Start()
    {
        Debug.Log("GO GO GO!!!!");
        UpdateScreenReference(); // Find the active screen at startup

        timerText.text = $"Timer: {programTime:F0}";
        warningText.text = "TOUCH THE SCREEN";
        timerText.enabled = false;
        warningText.enabled = false;
    }

    void Update()
    {
        timerText.text = $"Timer: {programTime:F0}";

        if (Input.GetMouseButtonDown(0)){
            programTime = 20.0f; //if left click made reset timer 
            Debug.Log("reset bc click");
        }

        // checks which screen is active by object name in hierarchy
        if (screenControl.IsScreenActive("Start Screen") || screenControl.IsScreenActive("Photo Capture")){
            programTime = 20.0f; 
            // keeps timer constant on whichever screens we need
        }
        else{
            programTime -= Time.deltaTime; // run on other screens
        }

        // Moves the timer/warning text to active screen
        //makes both TMpro objects a child of the active screen
        UpdateScreenReference();
        UpdateTextPosition();

        if (programTime <= 0.0f){
            Debug.Log("out of time... bye :(");
            screenControl.Showscreen0();
        }

        // Show warning when time is ≤ 5s
        if (programTime <= 5.0f){
            timerText.enabled = true;
            warningText.enabled = true;
        }
        else{
            timerText.enabled = false;
            warningText.enabled = false;
        }
    }

    void UpdateScreenReference(){
        if (screenControl.screen0.activeInHierarchy)
            currentScreen = screenControl.screen0.transform;
        else if (screenControl.screen1.activeInHierarchy)
            currentScreen = screenControl.screen1.transform;
        else if (screenControl.screen2.activeInHierarchy)
            currentScreen = screenControl.screen2.transform;
        else if (screenControl.screen3.activeInHierarchy)
            currentScreen = screenControl.screen3.transform;
        //will need all screens here, not ideal but will find a workaround later
    }

    void UpdateTextPosition(){
        if (currentScreen != null)
        {
            timerText.transform.SetParent(currentScreen, false);
            warningText.transform.SetParent(currentScreen, false);
        }
    }
}
