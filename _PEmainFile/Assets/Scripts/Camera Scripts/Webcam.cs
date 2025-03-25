using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Webcam : MonoBehaviour
{
    public RawImage webby;
    public string filePath;

    public string name = "photo";

    public string savedPath;
    
    public int cnt = 0;
    public WebCamTexture webcamTexture;

    public int state = 0;
    public float programTime = 5.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI messageText;
    public ScreenControl screenControl; 
    private Transform currentScreen; 



    public void Start()
    {
        filePath = @"C:\Users\willi\Desktop\Class\CSCI495\CapstoneUnityProject\Photobooth-Experience\_PEmainFile\Assets\Photos";
        webcamTexture = new WebCamTexture();
        webby.texture = webcamTexture;
        webby.material.mainTexture = webcamTexture;
       // webcamTexture.Play();

                Debug.Log("GO GO GO!!!!");
        UpdateScreenReference(); // Find the active screen at startup

        timerText.text = $"{programTime:F0}";
        messageText.text = "Get Ready";
        timerText.enabled = false;
        messageText.enabled = false;
    }

        public void Photo0(){
        messageText.text = "Cool";
        messageText.enabled = true;
        
        if(programTime <= 0.0f){
          //  messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }


    }

    public void Photo1(){
       messageText.text = "Nice";
   //     messageText.enabled = true;
        
        if(programTime <= 0.0f){
     //       messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }


    }

    public void Photo2(){
       messageText.text = "Rad";
        //messageText.enabled = true;
        
        if(programTime <= 0.0f){
            
           // messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }




    }

    public void Photo3(){
       
      //  messageText.enabled = true;
        
        if(programTime <= 0){
      //      messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }
    }

    public void Update()
    {
        
        

        if(!(screenControl.IsScreenActive("Photo Capture")) ){
            messageText.enabled = false;
            programTime = 5.0f;
            if(screenControl.IsScreenActive("Tap to Begin Screen")){
                webcamTexture.Play();
            }
            else{
                webcamTexture.Stop(); //ask team do we really want camera off????
            }
            
        }
        else{

        messageText.enabled = true;
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


        if ((state == 0) && (programTime < 0.0f)){
            screenControl.Flash();
            Photo0();
            TakePhoto(); //switch cases
        }
        else if ((state == 1) && (programTime < 0.0f)){
            screenControl.Flash();
            Photo1();
            TakePhoto();
        }
        else if ((state == 2) && (programTime < 0.0f)){
             screenControl.Flash();
            Photo2();
            TakePhoto();
        }
        else if ((state == 3) && (programTime < 0.0f)){
             screenControl.Flash();
            Photo3();
            TakePhoto();
        }
        }


    }

 //   public void Update()
   // {
       // if
     //       if (Input.GetKeyDown(KeyCode.Space))
       //     {
         //       TakePhoto();
        //    }
   // }

    public void TakePhoto()
    {
        string newName = name + cnt;
        newName = newName + ".png";
        cnt++;
        Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
        photo.SetPixels(webcamTexture.GetPixels());
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();
        savedPath = Path.Combine(filePath, newName);
        File.WriteAllBytes(savedPath, bytes);

        Debug.Log("Photo saved at: " + savedPath);
    }

    public void OnDestroy()
    {
        if (webcamTexture != null)
            webcamTexture.Stop();
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
    }
    void UpdateTextPosition(){
        if (currentScreen != null)
        {
            timerText.transform.SetParent(currentScreen, false);
            messageText.transform.SetParent(currentScreen, false);
        }
    }
}
