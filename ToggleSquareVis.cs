using UnityEngine;

public class ToggleSquareVisibility : MonoBehaviour
{
    private bool isSquareVisible = true;  // Start with the square visible
    private SpriteRenderer spriteRenderer; // To reference the square's sprite renderer

    void Start()
    {
        // Get the SpriteRenderer component from the object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left-click
        {
            // Ensure the camera is assigned
            if (Camera.main == null)
            {
                Debug.LogError("Main Camera is not assigned.");
                return;
            }

            // Convert the mouse position to world space
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the click was on the square
            Collider2D clickedObject = Physics2D.OverlapPoint(mousePos);

            // If clicked on the square, make it invisible
            if (clickedObject != null && clickedObject.gameObject == gameObject)
            {
                // Make the square invisible (hide the sprite)
                isSquareVisible = false;
                spriteRenderer.enabled = isSquareVisible; // Disable the sprite renderer
                Debug.Log("Square is now invisible");
            }
            else if (!isSquareVisible) // If square is invisible and clicked anywhere else, make it visible again
            {
                isSquareVisible = true;
                spriteRenderer.enabled = isSquareVisible; // Enable the sprite renderer
                Debug.Log("Square is now visible");
            }
        }
    }
}
