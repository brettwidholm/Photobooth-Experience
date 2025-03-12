using TMPro;
using UnityEngine;

public class DisappearPls : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //finds textmeshpro in child object
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bool isVisible = !spriteRenderer.enabled;
            spriteRenderer.enabled = isVisible;
            
            if (textMesh != null)
                textMesh.enabled = isVisible;
        }
    }
}
