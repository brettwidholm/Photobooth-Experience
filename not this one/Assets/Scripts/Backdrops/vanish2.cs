using UnityEngine;


public class Vanish2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    public void Start()
    {
        Debug.Log("hellp from vanish2");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (State.GetState() != 1){
            spriteRenderer.enabled = false; 
        }         
            

        else if (State.GetState() == 1) 
        {
            spriteRenderer.enabled = true; 
        }



    }
    
}
