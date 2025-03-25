using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class EmailController : MonoBehaviour
{
    private int counter = 0; // Step tracker
    private SpriteRenderer spriteRenderer;
    public TMP_InputField emailInputField;

  //  public TMP_InputField nameInputField;
    public Button confirmButton;
    public TextMeshProUGUI emailDisplayText;

    public string gifFilePath = @"C:\Users\willi\Desktop\Class\CSCI495\CapstoneUnityProject\Photobooth-Experience\_PEmainFile\Assets\gif\rad.gif";
    private string userEmail = "";

    private string userName = "";
    private Camera mainCamera;

    // UI Elements (No Panel, Just Text and Buttons)
    public TextMeshProUGUI invalidText;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;
    public Button noButton;
    public Button startButton;  // Start button for initial click

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned.");
        }

        startButton.onClick.AddListener(OnStartButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);

        UpdateUI();
    }

    void Update()
    {
        // Detect screen click and reset if invalid email is shown
        if (counter == 3 && Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            OnInvalidEmailReset();
        }
    }

    // Triggered when the start button is clicked
    void OnStartButtonClick()
    {
        counter = 1; // Move to email input step
        startButton.gameObject.SetActive(false); // Hide the start button
        UpdateUI();
    }

    void OnConfirmButtonClick()
    {
        userEmail = emailInputField.text.Trim();
       // userName = nameInputField.text.Trim();

        if (!IsValidEmail(userEmail))
        {
            ShowInvalidMessage();
            return;
        }

        ShowConfirmationOptions();
    }

    void ShowInvalidMessage()
    {
        counter = 3; // Set state to invalid email message
        emailInputField.text = ""; // Clears the input field
        invalidText.gameObject.SetActive(true);
        emailInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }

    void ShowConfirmationOptions()
    {
        counter = 4; // Set state to confirmation step
        confirmationText.gameObject.SetActive(true);
        confirmationText.text = $"{userEmail}";
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

        emailInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }

    void OnYesButtonClick()
    {
        counter = 2; // Move to final display step
        SendEmail(userEmail); // Send an email to the provided address
        UpdateUI();
    }

    void OnNoButtonClick()
    {
        emailInputField.text = ""; // Clears the input field
        counter = 1; // Go back to input field
        UpdateUI();
    }

    bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    void SendEmail(string recipientEmail)
    {
        string senderEmail = "boothphoto57@gmail.com"; // Replace with your Gmail
        string senderPassword = "msfu xycd qnwz hilv"; // Use App Password

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(recipientEmail);
            mail.Subject = "Hi Brandon boy batista";
            mail.Body = "Here is your GIF! \n"; // Your email content here

            Attachment gifAttachment = new Attachment(gifFilePath);
            mail.Attachments.Add(gifAttachment);

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword) as ICredentialsByHost;
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
            Debug.Log("Email sent successfully to " + recipientEmail);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to send email: " + e.Message);
        }
    }

    void UpdateUI()
    {
        spriteRenderer.enabled = (counter == 0);
        emailInputField.gameObject.SetActive(counter == 1);
        confirmButton.gameObject.SetActive(counter == 1);
        emailDisplayText.gameObject.SetActive(counter == 2);
        emailDisplayText.text = (counter == 2) ? $"Email: {userEmail}" : "";

        invalidText.gameObject.SetActive(counter == 3);
        confirmationText.gameObject.SetActive(counter == 4);
        yesButton.gameObject.SetActive(counter == 4);
        noButton.gameObject.SetActive(counter == 4);
    }

    // Method to reset the invalid email state
    void OnInvalidEmailReset()
    {
        invalidText.gameObject.SetActive(false);  // Hide the invalid message
        emailInputField.gameObject.SetActive(true);  // Show the input field again
        confirmButton.gameObject.SetActive(true);  // Show the confirm button again
        emailInputField.text = "";  // Clear the email input
        counter = 1;  // Go back to the email input step
        UpdateUI();
    }

    //not the final product for touchscreen keyboard
    public void EmailInputSelect(){
        System.Diagnostics.Process.Start("tabtip.exe");

        //TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Enter your email address");
    }
}
