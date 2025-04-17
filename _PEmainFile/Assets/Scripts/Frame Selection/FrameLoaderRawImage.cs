using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FrameLoaderUI : MonoBehaviour
{
    public string folderPath;
    public RawImage rawImage; //frame selection window

    private List<string> imagePaths = new List<string>();
    private int currentIndex = 0;

    public Clicker1 c1; //frame1
    public Clicker2 c2; //frame2
    public Clicker3 c3; //frame3
    public Clicker4 c4; //frame4

    public PathGetter getter;

    void Start()
    {
        folderPath = Path.Combine(getter.getPath(), "Frames");

        if (rawImage == null)
            rawImage = GetComponent<RawImage>();

       // if (aspectFitter == null)
         //   aspectFitter = GetComponent<AspectRatioFitter>();

        LoadAllImagePaths();

        if (imagePaths.Count > 1)
        {
            LoadTexture(imagePaths[currentIndex]);
        }
        else
        {
            //add screen control thing
            Debug.LogWarning("No images found in Frames folder.");
        }
    }

    public void LoadAllImagePaths()
    {
        if (Directory.Exists(folderPath))
        {
            string none = getter.getPath() + @"\Logos\none.jpg";
            imagePaths.Add(none);
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLowerInvariant();
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                {
                    imagePaths.Add(file);
                }
            }
            folderPath = folderPath + @"\COFCBorders";
            files = Directory.GetFiles(folderPath);
                        foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLowerInvariant();
                if (extension == ".png")
                {
                    imagePaths.Add(file);
                }
            }


        }
        else
        {
            Debug.LogWarning("Frames folder not found: " + folderPath);
        }
    }

    public void LoadNextImage()
    {
        if (imagePaths.Count == 0) return;

        currentIndex = State.GetState() % imagePaths.Count;
        LoadTexture(imagePaths[currentIndex]);

        if (ClickChecker.getSelected() == 1){
            c1.LoadTexture(imagePaths[currentIndex], currentIndex);
        }
        else if (ClickChecker.getSelected() == 2){
            c2.LoadTexture(imagePaths[currentIndex], currentIndex);
        }
        else if (ClickChecker.getSelected() == 3){
            c3.LoadTexture(imagePaths[currentIndex], currentIndex);
        }
        else if (ClickChecker.getSelected() == 4){
            c4.LoadTexture(imagePaths[currentIndex], currentIndex);
        }
        
    }

    public void LoadTexture(string path)
    {
        byte[] imageBytes = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        rawImage.texture = texture;

        // Automatically update the aspect ratio
   //     if (aspectFitter != null)
       // {
     //       aspectFitter.aspectRatio = (float)texture.width / texture.height;
       // }
    }
}
