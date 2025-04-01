using System.IO;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public string folderPath = Path.Combine(Application.dataPath, "Photos"); // Fixed absolute path
    public float frameRate = 0.5f; // Speed of animation
    private Sprite[] frames;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;

    
    public float targetWidth = 1500f;
    public float targetHeight = 1500f;

       public void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //WJH^
       // LoadSprites();
    }

    public void Update()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
        }
        LockAspectRatio(spriteRenderer.sprite);
    }

    public void LoadSprites()
{
    if (spriteRenderer == null)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on GameObject! Cannot load sprites.");
            return;
        }
    }

    if (!Directory.Exists(folderPath))
    {
        Debug.LogError("Folder not found: " + folderPath);
        return;
    }

    string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
    frames = new Sprite[imageFiles.Length];

    for (int i = 0; i < imageFiles.Length; i++)
    {
        byte[] imageData = File.ReadAllBytes(imageFiles[i]);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        frames[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    Debug.Log(frames.Length + " sprites loaded from " + folderPath);
    LockAspectRatio(spriteRenderer.sprite);
}

/*
    public void LoadSprites()
    {
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("Folder not found: " + folderPath);
            return;
        }

        string[] imageFiles = Directory.GetFiles(folderPath, "*.png"); // Load only PNG images
        frames = new Sprite[imageFiles.Length];

        for (int i = 0; i < imageFiles.Length; i++)
        {
            byte[] imageData = File.ReadAllBytes(imageFiles[i]);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData); // Convert image to texture

            frames[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        Debug.Log(frames.Length + " sprites loaded from " + folderPath);
        LockAspectRatio(spriteRenderer.sprite);
    }
*/

    public void LockAspectRatio(Sprite sprite){
 
        if (sprite == null) return;

        // Get original sprite dimensions
        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        // Calculate scale needed to achieve 50x50
        float scaleX = targetWidth / spriteWidth;
        float scaleY = targetHeight / spriteHeight;

        // Apply the scale
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
        
    }
}
