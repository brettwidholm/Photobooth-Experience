using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Webcam : MonoBehaviour
{
    public RawImage webby;
    public string filePath = Path.Combine(Application.dataPath, "Photos");

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
        

//FIX?
    try
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 1){
            webcamTexture = new WebCamTexture();
            Debug.Log($"Only Camera Name: {devices[0].name} | Front-facing: {devices[0].isFrontFacing}");
        }
        else{
            for (int i = 0; i < devices.Length; i++){
                if(devices[i].name.Contains("Front")){
                    Debug.Log($"[CAMERA {i}] Name: {devices[i].name} | Front-facing: {devices[i].isFrontFacing}");
                    webcamTexture = new WebCamTexture(devices[i].name, 1280, 720, 30);
                    break;
                }
            }
        }
        
        //STANDARD LAPTOP WEBCAM CONFIG
        //webcamTexture = new WebCamTexture();

        //SURFACE CONFIG
        //webcamTexture = new WebCamTexture("Surface Camera Front", 1280, 720, 30);
        //Trying to make it detect whether rotation is needed

        Debug.Log($"Camera Name: {webcamTexture.deviceName}  Default Rotation: {webcamTexture.videoRotationAngle}");

        webby.texture = webcamTexture;
        webby.material.mainTexture = webcamTexture;

        webcamTexture.Play();
        
        AdjustPreviewOrientation();
        float aspectRatio = (float)webcamTexture.width / webcamTexture.height;

        if (aspectRatio > 1f)
        {
            float offsetX = (aspectRatio - 1f) / 2f / aspectRatio;
            webby.uvRect = new Rect(offsetX, 0f, 1f / aspectRatio, 1f);
        }
        else
        {
            float offsetY = (1f / aspectRatio - 1f) / 2f;
            webby.uvRect = new Rect(0f, offsetY, 1f, aspectRatio);
        }
    }
    catch{
        Debug.LogWarning("Preferred camera not found. Falling back to device list.");
        TryAutoSelectCamera();
    }

   
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

    public void TakePhoto()
    {

        string newName = name + cnt + ".png";
        cnt++;

        int width = webcamTexture.width;
        int height = webcamTexture.height;
        Color[] pixels = webcamTexture.GetPixels();

        // Create rotated texture 90deg clockwise
        Texture2D photo = new Texture2D(height, width);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                photo.SetPixel(height - y - 1, x, pixels[y * width + x]);
            }
        }

        photo.Apply();

        // Crop to center square
    Texture2D square = CropToSquare(photo);

    // Save to PNG
    byte[] bytes = square.EncodeToPNG();
    savedPath = Path.Combine(filePath, newName);
    File.WriteAllBytes(savedPath, bytes);

    Debug.Log("✅ Photo saved at: " + savedPath);
        /*
        byte[] bytes = photo.EncodeToPNG();
        savedPath = Path.Combine(filePath, newName);
        File.WriteAllBytes(savedPath, bytes);

        Debug.Log("Photo saved at: " + savedPath);
        */
        /*
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
        */
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

    void TryAutoSelectCamera(){
        WebCamDevice[] devices = WebCamTexture.devices;

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log($"[CAMERA {i}] Name: {devices[i].name} | Front-facing: {devices[i].isFrontFacing}");
        }

        int fallbackIndex = 0; // Start with the first, or read from file

        webcamTexture = new WebCamTexture(devices[fallbackIndex].name, 1280, 720, 30);
        webby.texture = webcamTexture;
        webby.material.mainTexture = webcamTexture;

        webcamTexture.Play();

        AdjustPreviewOrientation();
        float aspectRatio = (float)webcamTexture.width / webcamTexture.height;

        if (aspectRatio > 1f)
        {
            float offsetX = (aspectRatio - 1f) / 2f / aspectRatio;
            webby.uvRect = new Rect(offsetX, 0f, 1f / aspectRatio, 1f);
        }
        else
        {
            float offsetY = (1f / aspectRatio - 1f) / 2f;
            webby.uvRect = new Rect(0f, offsetY, 1f, aspectRatio);
        }

            }

        void AdjustPreviewOrientation()
        {
            if(webcamTexture.videoRotationAngle == 0 && webcamTexture.deviceName.Contains("Surface")){
                // Rotate 90° counter-clockwise
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, 90f);
                // No flipping
                webby.rectTransform.localScale = new Vector3(1, 1, 1);
                Debug.Log("Rotation 90°, no flip");
            }
            else if(webcamTexture.videoRotationAngle == 0){
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, 0f);
            }
            else if(webcamTexture.videoRotationAngle == 90){
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, 90f);
            }
            else if(webcamTexture.videoRotationAngle == 180){
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, 180f);
            }
            else if(webcamTexture.videoRotationAngle == 270){
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, 270f);
            }
            Debug.Log($"Camera Name: {webcamTexture.deviceName}  New Rotation: {webcamTexture.videoRotationAngle}");
/*

            */
        }

        private Texture2D CropToSquare(Texture2D original)
        {
            int size = Mathf.Min(original.width, original.height);
            int x = (original.width - size) / 2;
            int y = (original.height - size) / 2;

            Color[] croppedPixels = original.GetPixels(x, y, size, size);
            Texture2D square = new Texture2D(size, size);
            square.SetPixels(croppedPixels);
            square.Apply();
            return square;
        }


}
