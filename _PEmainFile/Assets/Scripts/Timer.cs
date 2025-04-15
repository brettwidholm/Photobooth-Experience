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
    public ResetScene resetScene; 

    void Start()
    {
        
        UpdateScreenReference(); // Find the active screen at startup

        timerText.text = $"Timer: {programTime:F0}";
        warningText.text = "TOUCH THE SCREEN";
        timerText.enabled = false;
        warningText.enabled = false;
        screenControl.timerPanel.SetActive(false); // Hide the timer panel at the start
    }

    void Update()
    {
        timerText.text = $"Timer: {programTime:F0}";

        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown){
            programTime = 20.0f; //if left click made reset timer 
        }

        // checks which screen is active by object name in hierarchy
        if (screenControl.IsScreenActive("Start Screen") || screenControl.IsScreenActive("Photo Capture") || screenControl.IsScreenActive("Loading Screen")){
            programTime = 20.0f;   
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
            resetScene.RestartScene();
        }

        // Show warning when time is ≤ 5s
        if (programTime <= 5.0f){
            screenControl.timerPanel.SetActive(true); // Show the timer panel
            timerText.enabled = true;
            warningText.enabled = true;
        }
        else{
            screenControl.timerPanel.SetActive(false); // Hide the timer panel
            timerText.enabled = false;
            warningText.enabled = false;
        }
    }

    void UpdateScreenReference(){
        foreach (Transform screen in screenControl.transform){
            if (screen.gameObject.activeInHierarchy){
                currentScreen = screen;
                break;
            }
        }
    }

    void UpdateTextPosition(){
        if (currentScreen != null){
            timerText.transform.SetParent(currentScreen, false);
            warningText.transform.SetParent(currentScreen, false);
        }
    }
}
