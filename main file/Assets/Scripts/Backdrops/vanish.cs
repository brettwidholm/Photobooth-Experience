using UnityEngine;


public class Vanish : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.enabled = false; 
    }

    public void Update()
    {
        if (State.GetState() != 0){
            spriteRenderer.enabled = false; 
        }         
            

        else if (State.GetState() == 0) 
        {
            spriteRenderer.enabled = true; 
        }
    }
    
}
