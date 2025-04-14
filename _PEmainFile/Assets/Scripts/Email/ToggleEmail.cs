using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;

public class EmailController : MonoBehaviour
{
    private int counter = 0;
    public TMP_InputField emailInputField;
    public TMP_InputField firstNameInputField;
    public TMP_InputField lastNameInputField;
    public Button confirmButton;
    public TextMeshProUGUI emailDisplayText;

    public string gifFilePath;
    private string userEmail = "";
    private string userFirstName = "";
    private string userLastName = "";

    public TextMeshProUGUI invalidText;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;
    public Button noButton;
    public Button startButton;

    public PathGetter getter;


    void Start()
    {
       gifFilePath = Path.Combine(getter.getPath(), "gif\\rad.gif"); // Path to the GIF file

        startButton.onClick.AddListener(OnStartButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);

        emailInputField.onValueChanged.AddListener(delegate { ValidateEmail(); });
        confirmButton.interactable = false;
       
        UpdateUI();
    }

    void Update()
    {
        if (counter == 3 && Input.GetMouseButtonDown(0))
        {
            OnInvalidEmailReset();
        }
    }

    void OnStartButtonClick()
    {
        counter = 1;
        startButton.gameObject.SetActive(false);
        UpdateUI();
    }

    void OnConfirmButtonClick()
    {
        userEmail = emailInputField.text.Trim();
        userFirstName = firstNameInputField.text.Trim();
        userLastName = lastNameInputField.text.Trim();

        if (!IsValidEmail(userEmail))
        {
            ShowInvalidMessage();
            return;
        }

        ShowConfirmationOptions();
    }

    void ShowInvalidMessage()
    {
        counter = 3;
        emailInputField.text = "";
        invalidText.gameObject.SetActive(true);
        emailInputField.gameObject.SetActive(false);
        firstNameInputField.gameObject.SetActive(false);
        lastNameInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }

    void ShowConfirmationOptions()
    {
        counter = 4;
        confirmationText.gameObject.SetActive(true);
        confirmationText.text = $"Name: {userFirstName} {userLastName}\nEmail: {userEmail}";
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
       
        emailInputField.gameObject.SetActive(false);
        firstNameInputField.gameObject.SetActive(false);
        lastNameInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }

    void OnYesButtonClick()
    {
        counter = 2;
        SendEmail(userEmail, userFirstName, userLastName);
        UpdateUI();
    }

    void OnNoButtonClick()
{

    // Reset the counter and update the UI to go back to the input fields
    counter = 1;

    // Ensure the input fields are re-enabled and cleared
    emailInputField.gameObject.SetActive(true);
    firstNameInputField.gameObject.SetActive(true);
    lastNameInputField.gameObject.SetActive(true);
    confirmButton.gameObject.SetActive(true);
   
    // Hide the confirmation text and buttons
    confirmationText.gameObject.SetActive(false);
    yesButton.gameObject.SetActive(false);
    noButton.gameObject.SetActive(false);
   
    UpdateUI();
}


    bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    void ValidateEmail()
    {
        confirmButton.interactable = IsValidEmail(emailInputField.text.Trim());
    }
  void SendEmail(string recipientEmail, string firstName, string lastName)
    {
        string senderEmail = "boothphoto57@gmail.com";
        string senderPassword = "msfu xycd qnwz hilv";

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(recipientEmail);
            if (firstName == "" && lastName == ""){
                mail.Subject = $"Hello! Here is your GIF!";
            }
            else{
            mail.Subject = $"Hello, {firstName} {lastName}! Here is your GIF!";
            }
            mail.Body = "Thank you for participating at our event, enjoy! \n";
           
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
    /*
    void SendEmail(string recipientEmail, string firstName, string lastName)
    {
        yesButton.interactable = false;
        string senderEmail = "boothphoto57@gmail.com";
        string senderPassword = "msfu xycd qnwz hilv";

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(recipientEmail);

            if (!(firstName == "" && lastName == "")){
                mail.Subject = $"Hello, {firstName} {lastName}! Here is your GIF!";
            }
            else if(firstName != "" && lastName == ""){
                mail.Subject = $"Hello, {firstName}! Here is your GIF!";
            }

            mail.Body = "Here is your GIF!\n";
           
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
        //yesButton.interactable = true;
    }
*/
    void UpdateUI()
    {
        emailInputField.gameObject.SetActive(counter == 1);
        firstNameInputField.gameObject.SetActive(counter == 1);
        lastNameInputField.gameObject.SetActive(counter == 1);
        confirmButton.gameObject.SetActive(counter == 1);
        emailDisplayText.gameObject.SetActive(counter == 2);
        emailDisplayText.text = (counter == 2) ? $"Email: {userEmail}" : "";

        invalidText.gameObject.SetActive(counter == 3);
        confirmationText.gameObject.SetActive(counter == 4);
        yesButton.gameObject.SetActive(counter == 4);
        noButton.gameObject.SetActive(counter == 4);
    }

    void OnInvalidEmailReset()
    {
        invalidText.gameObject.SetActive(false);
        emailInputField.gameObject.SetActive(true);
        firstNameInputField.gameObject.SetActive(true);
        lastNameInputField.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
        emailInputField.text = "";
        firstNameInputField.text = "";
        lastNameInputField.text = "";
        counter = 1;
        UpdateUI();
    }
}
