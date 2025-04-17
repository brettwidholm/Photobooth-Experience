using UnityEngine;
public class ForwardButton : MonoBehaviour
{



    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public FrameLoaderUI frame;


    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider= GetComponent<BoxCollider2D>();
    }

    
    public void OnMouseDown()
    {
        State.IncrementState();
        frame.LoadNextImage();

       // backdrop0.Update();  // Call the function in the other script
    }


}
