using UnityEngine;
public class Button1 : MonoBehaviour
{
 //  private Vanish backdrop0; 
  // private Vanish2 backdrop1; 


    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;


    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider= GetComponent<BoxCollider2D>();
    }

    
    public void OnMouseDown()
    {
        State.IncrementState();

       // backdrop0.Update();  // Call the function in the other script
    }

    public void Update(){
        if (State.GetState() != 3){
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }
        else if (State.GetState() == 3){
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }



}
