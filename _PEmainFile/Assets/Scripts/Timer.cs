using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float programTime = 20.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI warningText;
    public ScreenControl screenControl; 
    private Transform currentScreen; 

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
            programTime = 20.0f;   // keeps timer constant on whichever screens we need
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
        foreach (Transform screen in screenControl.transform)
        {
            if (screen.gameObject.activeInHierarchy)
            {
                currentScreen = screen;
                break;
            }
        }
        /* //This old method was overkill but leaving here just in case
        if (screenControl.screen0.activeInHierarchy)
            currentScreen = screenControl.screen0.transform;
        else if (screenControl.screen1.activeInHierarchy)
            currentScreen = screenControl.screen1.transform;
        else if (screenControl.screen2.activeInHierarchy)
            currentScreen = screenControl.screen2.transform;
        else if (screenControl.screen3.activeInHierarchy)
            currentScreen = screenControl.screen3.transform;
        //will need all screens here, not ideal but will find a workaround later
        */
    }

    void UpdateTextPosition(){
        if (currentScreen != null)
        {
            timerText.transform.SetParent(currentScreen, false);
            warningText.transform.SetParent(currentScreen, false);
        }
    }
}
