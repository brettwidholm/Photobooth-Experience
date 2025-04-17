using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Collections;

public class Webcam : MonoBehaviour
{
    public RawImage webby;
    private string filePath; 

    public new string name = "photo";

    public string savedPath;
    
    public int cnt = 0;
    public WebCamTexture webcamTexture;
    private int previewRotationAngle = 0;


    public int state = 0;
    public float programTime = 5.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI messageText;
    public ScreenControl screenControl; 
    private Transform currentScreen; 

    public PathGetter getter;

    Texture2D frame;

    public bool frameExist = false;
    public bool borderExist = false;

    public Clicker1 c1;
    public Clicker2 c2;
    public Clicker3 c3;
    public Clicker4 c4;

    void Awake()
    {
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
        }
        catch{
            Debug.LogWarning("Preferred camera not found. Falling back to device list.");
            TryAutoSelectCamera();
        }
    }

    public void Start()
    {
        filePath = Path.Combine(getter.getPath(), "Photos");
        Debug.Log("File path: " + filePath);
        
        UpdateScreenReference(); // Find the active screen at startup

        timerText.text = $"{programTime:F0}";
        messageText.text = "Get Ready";
        timerText.enabled = false;
        messageText.enabled = false;
    }

        public void Photo0(){
        messageText.text = "Cool";
        messageText.enabled = true;

        if(!(c1.getCurrentFrame().Equals("None"))){
            if(!(c1.getCurrentFrame().Contains("COFCBorders"))){
                frameExist = true;
                frame = LoadTexture(c1.getCurrentFrame());
            }
            else{
                borderExist = true;
                frame = LoadTexture(c1.getCurrentFrame());

            }


        }
        
        
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

        if(!(c2.getCurrentFrame().Equals("None"))){
            if(!(c2.getCurrentFrame().Contains("COFCBorders"))){
                frameExist = true;
                frame = LoadTexture(c2.getCurrentFrame());
            }
            else{
                borderExist = true;
                frame = LoadTexture(c2.getCurrentFrame());

            }
        }
        
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
        if(!(c3.getCurrentFrame().Equals("None"))){
            if(!(c3.getCurrentFrame().Contains("COFCBorders"))){
                frameExist = true;
                frame = LoadTexture(c3.getCurrentFrame());
            }
            else{
                borderExist = true;
                frame = LoadTexture(c3.getCurrentFrame());

            }
        }
        
        if(programTime <= 0.0f){
           // messageText.text = "click";
            timerText.enabled = false;
            state++;
            programTime = 5.0f;
        }
    }

    public void Photo3(){

        if(!(c4.getCurrentFrame().Equals("None"))){
            if(!(c4.getCurrentFrame().Contains("COFCBorders"))){
                frameExist = true;
                frame = LoadTexture(c4.getCurrentFrame());
            }
            else{
                borderExist = true;
                frame = LoadTexture(c4.getCurrentFrame());

            }
        }
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
        }
        else{
            messageText.enabled = true;
            /* if (state > 3)
                {
                    StartCoroutine(TriggerScreen4WithLoading());
                    enabled = false; // disable further Update loop once done
                    return;// exit early
                } */
            
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

                StartCoroutine(TriggerScreen4WithLoading());
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
//think this makes it right on all devices
        Texture2D photo;

        if (previewRotationAngle == 90)
        {
            photo = new Texture2D(height, width);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    photo.SetPixel(height - y - 1, x, pixels[y * width + x]);
                }
            }
        }
        else if (previewRotationAngle == 180)
        {
            photo = new Texture2D(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    photo.SetPixel(width - x - 1, height - y - 1, pixels[y * width + x]);
                }
            }
        }
        else if (previewRotationAngle == 270)
        {
            photo = new Texture2D(height, width);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    photo.SetPixel(y, width - x - 1, pixels[y * width + x]);
                }
            }
        }
        else // 0 or unknown
        {
            photo = new Texture2D(width, height);
            photo.SetPixels(pixels);
        }

        photo.Apply();

/*
        // Create rotated texture 90deg 
        Texture2D photo = new Texture2D(height, width);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                photo.SetPixel(height - y - 1, x, pixels[y * width + x]);
            }
        }

        photo.Apply();
*/
        // Crop to center square
        Texture2D square = CropToSquare(photo);

        if(frameExist){
            Texture2D finalPhoto = ApplyCircularMask(square);
            Texture2D finalphotofr = MergeImages(finalPhoto, frame);
            
            byte[] bytes = finalphotofr.EncodeToPNG();
            savedPath = Path.Combine(filePath, newName);
            File.WriteAllBytes(savedPath, bytes);

            Debug.Log("✅ Photo saved at: " + savedPath);

        }
        else if(borderExist){
            Texture2D finalphotofr = MergeImages(frame, square);
            
            byte[] bytes = finalphotofr.EncodeToPNG();
            savedPath = Path.Combine(filePath, newName);
            File.WriteAllBytes(savedPath, bytes);

            Debug.Log("✅ Photo saved at: " + savedPath);

        }

        else{
            byte[] bytes = square.EncodeToPNG();
            savedPath = Path.Combine(filePath, newName);
            File.WriteAllBytes(savedPath, bytes);

            Debug.Log("✅ Photo saved at: " + savedPath);

        }

        frameExist = false;
        borderExist = false;

        

        // Save to PNG


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
        StartWebcamFeed(); // Start the webcam feed with the selected camera
        Debug.Log($"Fallback camera selected: {devices[fallbackIndex].name}");

    }

        void AdjustPreviewOrientation()
        {
            if (webcamTexture.videoRotationAngle == 0 && webcamTexture.deviceName.Contains("Surface"))
            {
                previewRotationAngle = 90;
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, 90f);
                webby.rectTransform.localScale = new Vector3(1, -1, 1);
                Debug.Log("Surface cam: forcing 90° rotation");
            }
            else
            {
                previewRotationAngle = webcamTexture.videoRotationAngle;
                webby.rectTransform.localEulerAngles = new Vector3(0, 0, previewRotationAngle);
                webby.rectTransform.localScale = new Vector3(1, 1, 1);
            }

            Debug.Log($"Camera: {webcamTexture.deviceName} | Applied rotation: {previewRotationAngle}");
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

public Texture2D ApplyCircularMask(Texture2D source)
{
    int width = source.width;  // 1500
    int height = source.height; // 1500
    Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false);

    int centerX = width / 2;
    int centerY = height / 2;
    int radius = (width - 200) / 2; // Main visible radius
    int featherWidth = 50; // Width of the feathered edge

    float maxDistance = radius + featherWidth;

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            float dx = x - centerX;
            float dy = y - centerY;
            float distance = Mathf.Sqrt(dx * dx + dy * dy);

            Color pixel = source.GetPixel(x, y);

            if (distance > maxDistance)
            {
                // Fully transparent
                pixel.a = 0;
            }
            else if (distance > radius)
            {
                // Feather alpha based on distance
                float t = (distance - radius) / featherWidth; // 0 to 1
                pixel.a *= 1 - t;
            }

            result.SetPixel(x, y, pixel);
        }
    }

    result.Apply();
    return result;
}


    Texture2D LoadTexture(string path)
{
    byte[] fileData = File.ReadAllBytes(path);
    Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
    texture.LoadImage(fileData);
    return texture;
}

Texture2D MergeImages(Texture2D foreground, Texture2D background)
{
    int width = foreground.width;
    int height = foreground.height;

    // Create a RenderTexture for resizing
    RenderTexture rt = new RenderTexture(width, height, 32);
    RenderTexture.active = rt;
    Graphics.Blit(background, rt);

    // Convert RenderTexture back to Texture2D
    Texture2D resizedBackground = new Texture2D(width, height, TextureFormat.RGBA32, false);
    resizedBackground.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    resizedBackground.Apply();

    RenderTexture.active = null;
    rt.Release();

    // Create final merged image
    Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false);

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            Color fgPixel = foreground.GetPixel(x, y);
            Color bgPixel = resizedBackground.GetPixel(x, y);

            // Blend: Use the foreground pixel if not transparent, otherwise use background
            Color finalPixel = Color.Lerp(bgPixel, fgPixel, fgPixel.a);
            result.SetPixel(x, y, finalPixel);
        }
    }

    result.Apply();
    return result;
}

    public void StartWebcamFeed()
{
        Debug.Log($"Camera Name: {webcamTexture.deviceName}  Default Rotation: {webcamTexture.videoRotationAngle}");

            webby.texture = webcamTexture;
            webby.material.mainTexture = webcamTexture;

            webcamTexture.Play();
            //
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
    public void StopWebcamFeed()
{
    if (webcamTexture != null && webcamTexture.isPlaying)
    {
        
        screenControl.websosa.SetActive(false); // Hide the webcam feed
        webcamTexture.Stop();
        Debug.Log("✅ Webcam feed stopped manually.");
    }

}
private IEnumerator TriggerScreen4WithLoading()
{
    Debug.Log("Triggering Screen 4 with loading screen");

    yield return new WaitForEndOfFrame(); // render flash first
    screenControl.flash.SetActive(false);

    yield return new WaitForSeconds(0.1f); // tiny delay for comfort
    StopWebcamFeed();

    // Show loading screen immediately   
    screenControl.RunWithLoadingScreen(
        onComplete: () => screenControl.ShowScreen4(),
        onStart: () => {
            screenControl.giffy.ConvertImagesToGif();
            screenControl.loader.LoadSprites();
        },
        delay: 3.0f
    );
}



/*     private IEnumerator TriggerScreen4WithLoading()
{
    Debug.Log("Triggering Screen 4 with loading screen");
    yield return null; // optional small delay to let last photo process

    
        screenControl.giffy.ConvertImagesToGif();
        screenControl.loader.LoadSprites();
        screenControl.RunWithLoadingScreen(() => screenControl.ShowScreen4(), null, 3.0f);
        StopWebcamFeed(); // Stop the webcam feed after taking the last photo
        //screenControl.gifPrev.SetActive(true); // Show the GIF preview screen

} */
}
