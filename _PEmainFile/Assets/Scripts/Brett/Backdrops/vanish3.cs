using UnityEngine;


public class Vanish3 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (State.GetState() >= 3){
            spriteRenderer.enabled = false; 
        }         
            

        else if (State.GetState() == 2) 
        {
            spriteRenderer.enabled = true; 
        }

        else {
            spriteRenderer.enabled = false; 
        }

    }
    
}
