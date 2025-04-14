using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Mime;

public class EmailController : MonoBehaviour
{
    private int counter = 0;
    private SpriteRenderer spriteRenderer;
    public TMP_InputField emailInputField;
    public TMP_InputField firstNameInputField;
    public TMP_InputField lastNameInputField;
    public Button confirmButton;
    public TextMeshProUGUI emailDisplayText;

    public string gifFilePath;
    private string userEmail = "";
    private string userFirstName = "";
    private string userLastName = "";
    private Camera mainCamera;

    public TextMeshProUGUI invalidText;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;
    public Button noButton;
    public Button startButton;

    public PathGetter getter;

    void Start()
    {
        gifFilePath = Path.Combine(getter.getPath(), "gif\\rad.gif");

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
        counter = 1;
        emailInputField.gameObject.SetActive(true);
        firstNameInputField.gameObject.SetActive(true);
        lastNameInputField.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);

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
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(recipientEmail);

            // Set the subject
            mail.Subject = string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)
                ? "Your College of Charleston GIF!"
                : $"Hi {firstName} {lastName}, here’s your College of Charleston GIF!";

            // Set plain text fallback
            mail.Body = "Hey there, here's your personalized event GIF! Thanks for stopping by – we hope you had fun!";
            mail.IsBodyHtml = true;

            // Add the HTML body
            AlternateView htmlBody = EmailTemplate.GetHtmlBody(firstName, lastName, gifFilePath);
            mail.AlternateViews.Add(htmlBody);

            // Add the GIF attachment
            using (Attachment gifAttachment = new Attachment(gifFilePath))
            {
                mail.Attachments.Add(gifAttachment);

                // Configure the SMTP client
                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword) as ICredentialsByHost;
                    smtpServer.EnableSsl = true;

                    // Send the email
                    smtpServer.Send(mail);
                    Debug.Log("Email sent successfully to " + recipientEmail);
                }
            }
        }
    }
    catch (System.Exception e)
    {
        Debug.LogError("Failed to send email: " + e.Message);
    }
}
/*
    void SendEmail(string recipientEmail, string firstName, string lastName)
    {
        string senderEmail = "boothphoto57@gmail.com";
        string senderPassword = "msfu xycd qnwz hilv";

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(recipientEmail);

            mail.Subject = string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)
                ? "Your College of Charleston GIF!"
                : $"Hi {firstName} {lastName}, here’s your College of Charleston GIF!";

            mail.IsBodyHtml = true;
            mail.AlternateViews.Add(EmailTemplate.GetHtmlBody(firstName, lastName, gifFilePath));

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

public static class EmailTemplate
{
    public static AlternateView GetHtmlBody(string firstName, string lastName, string gifPath)
    {
        LinkedResource logo = new LinkedResource("Assets/Logos/CofC.png", MediaTypeNames.Image.Jpeg)
        {
            ContentId = "cofcLogo"
        };

        string html = $@"
        <body style='background-color:white; font-family: Arial, sans-serif;'>
            <div style='text-align: center; margin-top: 30px;'>
                <img src='cid:cofcLogo' width='150' />
                <h2>College of Charleston</h2>
                <p style='font-size: 18px;'>Hey {(string.IsNullOrWhiteSpace(firstName) ? "there" : firstName)}, here's your personalized event GIF!</p>
                <p style='font-size: 16px;'>Thanks for stopping by – we hope you had fun!</p>
                <p style='font-size: 16px;'>Do not forget to tag us with <strong>#CougarPride</strong> when you share your GIF.</p>
            </div>
        </body>";

        AlternateView avHtml = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
        avHtml.LinkedResources.Add(logo);
        return avHtml;
    }
}
