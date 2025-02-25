using UnityEngine;
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
        State.IncrementState();

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
