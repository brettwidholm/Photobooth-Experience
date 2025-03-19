using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhotoTime : MonoBehaviour
{
    public int state = 0;
    public float programTime = 5.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI messageText;
    public ScreenControl screenControl; 
    private Transform currentScreen; 

    public Webcam webby;

    public void Start()
    {
        webby.Start();
        Debug.Log("GO GO GO!!!!");
        UpdateScreenReference(); // Find the active screen at startup

        timerText.text = $"{programTime:F0}";
        messageText.text = "Get Ready";
        timerText.enabled = false;
        messageText.enabled = false;

    }

    public void Photo0(){
        
        messageText.enabled = true;
        
        if(programTime <= 0.0f){
            messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }


    }

    public void Photo1(){
       messageText.text = "Cool";
   //     messageText.enabled = true;
        
        if(programTime <= 0.0f){
            messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }


    }

    public void Photo2(){
       messageText.text = "Nice";
        //messageText.enabled = true;
        
        if(programTime <= 0.0f){
            
            messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }




    }

    public void Photo3(){
       messageText.text = "Rad";
      //  messageText.enabled = true;
        
        if(programTime <= 0){
            messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }

        


    }
    public void Update()
    {
        if (state > 3){
            screenControl.ShowScreen4();
        }
        
        timerText.text = $"{programTime:F0}";
        programTime -= Time.deltaTime;
        
        UpdateScreenReference();
        UpdateTextPosition();

        if(programTime <= 3.0f){
            timerText.enabled = true;
        }
        else{
            timerText.enabled = false;
        }


        if (state == 0){
            Photo0();
            webby.TakePhoto();
        }
        else if (state == 1){
            Photo1();
            webby.TakePhoto();
        }
        else if (state == 2){
            Photo2();
            webby.TakePhoto();
        }
        else if (state == 3){
            Photo3();
            webby.TakePhoto();
        }


    }

    public void UpdateScreenReference(){
        foreach (Transform screen in screenControl.transform)
        {
            if (screen.gameObject.activeInHierarchy)
            {
                currentScreen = screen;
                break;
            }
        }
        /* //old method was redundant but leaving here just in case
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
            messageText.transform.SetParent(currentScreen, false);
        }
    }
}
