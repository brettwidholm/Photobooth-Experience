using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro

public class ToggleSquareAndEmail : MonoBehaviour
{
    private int counter = 0; // Step tracker
    private SpriteRenderer spriteRenderer;
    public TMP_InputField emailInputField; // Reference to email input field
    public Button confirmButton; // Reference to confirm button
    public TextMeshProUGUI emailDisplayText; // Reference to text to display email
    private string userEmail = ""; // Store email entered by user

    void Start()
    {
        // Get the SpriteRenderer component from the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set initial states based on counter
        UpdateUI();

        // Add listener to confirm button
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left-click
        {
            if (Camera.main == null)
            {
                Debug.LogError("Main Camera is not assigned.");
                return;
            }

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D clickedObject = Physics2D.OverlapPoint(mousePos);

            if (counter == 0 && clickedObject != null && clickedObject.gameObject == gameObject)
            {
                counter++; // Move to next step (show email input and button)
                UpdateUI();
            }
            else if (counter == 2)
            {
                counter = 0; // Reset sequence
                UpdateUI();
            }
        }
    }

    void OnConfirmButtonClick()
    {
        userEmail = emailInputField.text.Trim(); // Store entered email

        if (string.IsNullOrEmpty(userEmail) || !userEmail.Contains("@") || !userEmail.Contains("."))
        {
            // Show error message if email is invalid	
            emailDisplayText.color = Color.red;
            emailDisplayText.text = "Invalid Email";
	    counter++;
        }
        else
        {
            // Valid email entered
            emailDisplayText.color = Color.green;
            emailDisplayText.text = "Email: " + userEmail;
            counter++; // Move to next step (display email)
        }

        emailInputField.text = ""; // Clear the input field
        UpdateUI();
    }

    void UpdateUI()
    {
        // Control visibility based on counter
        spriteRenderer.enabled = (counter == 0);
        emailInputField.gameObject.SetActive(counter == 1);
        confirmButton.gameObject.SetActive(counter == 1);
        emailDisplayText.gameObject.SetActive(counter == 2);
    }
}
