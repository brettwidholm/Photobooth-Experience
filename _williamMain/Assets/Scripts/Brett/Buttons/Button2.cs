using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.UI;
public class Button2 : MonoBehaviour
{
 //  private Vanish backdrop0; 
  // private Vanish2 backdrop1; 


    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;


    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    
    public void OnMouseDown()
    {
        string folderPath = @"C:\Users\yosha\main file\Assets\Frames"; // Change this to your folder path
        Process.Start("explorer.exe", folderPath);

       // backdrop0.Update();  // Call the function in the other script
    }

    public void Update(){
        if (State.GetState() == 2){
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }
        else if (State.GetState() != 2){
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }



}
