using UnityEngine;


public class BG1 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // state st = gameObject.GetComponent<state>();
    }

    void Vanish()
    {
        spriteRenderer.enabled = false;
    }

    void Appear(){
        spriteRenderer.enabled = true;
    }
}
