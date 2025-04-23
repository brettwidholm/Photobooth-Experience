using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Mime;
using System.Collections;

public class EmailController : MonoBehaviour
{
    private int counter = 0;
    public bool isSendingEmail = false;

    public TMP_InputField emailInputField;
    public TMP_InputField firstNameInputField;
    public TMP_InputField lastNameInputField;
    public Button confirmButton;
    public Button startButton;
    public TextMeshProUGUI emailDisplayText;

    public string gifFilePath;
    private string userEmail = "";
    private string userFirstName = "";
    private string userLastName = "";

    public TextMeshProUGUI invalidText;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;
    public Button noButton;

    public Toggle policyToggle;
    public TextMeshProUGUI privacyPolicyText;

    public PathGetter getter;
    public ScreenControl screenControl;

    void Start()
    {
        UnityMainThreadDispatcher.Init();

        gifFilePath = Path.Combine(getter.getPath(), "gif\\CofC.gif");

        startButton.onClick.AddListener(OnStartButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);
        emailInputField.onValueChanged.AddListener(delegate { ValidateEmail(); });
        policyToggle.onValueChanged.AddListener(delegate { ValidateEmail(); });

        // Setup clickable privacy text
        privacyPolicyText.text = "<link=\"privacy\">By using this program you agree to our <u><color=#800000>privacy policy</color></u></link>";
        privacyPolicyText.richText = true;
        privacyPolicyText.enabled = true;
        privacyPolicyText.ForceMeshUpdate();

        confirmButton.interactable = false;
        UpdateUI();
    }

   void Update()
{
    if (counter == 3 && Input.GetMouseButtonDown(0))
    {
        OnInvalidEmailReset();
    }

    // Only react to clicks that are actually over the link
    if (privacyPolicyText.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
{
    Vector3 worldMousePos = Input.mousePosition;
    Camera cam = Camera.main;

    if (RectTransformUtility.RectangleContainsScreenPoint(
        privacyPolicyText.rectTransform, worldMousePos, cam))
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(privacyPolicyText, worldMousePos, cam);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = privacyPolicyText.textInfo.linkInfo[linkIndex];
            if (linkInfo.GetLinkID() == "privacy")
            {
                screenControl.ShowPrivacyPolicyFromEmail();
            }
        }
    }
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
        policyToggle.gameObject.SetActive(false);
        privacyPolicyText.gameObject.SetActive(false);
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
        policyToggle.gameObject.SetActive(false);
        privacyPolicyText.gameObject.SetActive(false);
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
        policyToggle.gameObject.SetActive(true);
        privacyPolicyText.gameObject.SetActive(true);
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
        confirmButton.interactable = IsValidEmail(emailInputField.text.Trim()) && policyToggle.isOn;
    }

    public void SendEmail(string recipientEmail, string firstName, string lastName)
    {
        if (isSendingEmail) return;
        isSendingEmail = true;

        screenControl.RunWithLoadingScreen(
            onComplete: () =>
            {
                screenControl.loadingBar?.CompleteLoading();
                screenControl.StartCoroutine(WaitThenShowSuccess());
                isSendingEmail = false;
            },
            onStart: () =>
            {
                System.Threading.Thread emailThread = new System.Threading.Thread(() =>
                {
                    string senderEmail = "boothphoto57@gmail.com";
                    string senderPassword = "msfu xycd qnwz hilv";

                    try
                    {
                        if (!File.Exists(gifFilePath))
                        {
                            Debug.LogError("❌ GIF not found at: " + gifFilePath);
                            return;
                        }

                        using (MailMessage mail = new MailMessage())
                        using (Attachment gifAttachment = new Attachment(gifFilePath))
                        {
                            mail.From = new MailAddress(senderEmail);
                            mail.To.Add(recipientEmail);

                            mail.Subject = string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)
                                ? "Your College of Charleston GIF!"
                                : $"Hi {firstName} {lastName}, here’s your College of Charleston GIF!";

                            mail.Body = "Hey there, here's your personalized event GIF! Thanks for stopping by – we hope you had fun!";
                            mail.IsBodyHtml = true;

                            AlternateView htmlBody = EmailTemplate.GetHtmlBody(firstName, lastName, gifFilePath);
                            mail.AlternateViews.Add(htmlBody);
                            mail.Attachments.Add(gifAttachment);

                            using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                            {
                                smtpServer.Port = 587;
                                smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword);
                                smtpServer.EnableSsl = true;
                                smtpServer.Send(mail);
                            }
                        }

                        UnityMainThreadDispatcher.Enqueue(() => {
                            Debug.Log("✅ Email sent successfully to " + recipientEmail);
                        });
                    }
                    catch (System.Exception e)
                    {
                        UnityMainThreadDispatcher.Enqueue(() => {
                            Debug.LogError("❌ Failed to send email: " + e.Message);
                        });
                    }
                });

                emailThread.Start();
            },
            delay: 2.0f
        );
    }

    private IEnumerator WaitThenShowSuccess()
    {
        yield return new WaitForSeconds(0.5f);
        screenControl.ShowScreen7();
    }

    void UpdateUI()
    {
        emailInputField.gameObject.SetActive(counter == 1);
        firstNameInputField.gameObject.SetActive(counter == 1);
        lastNameInputField.gameObject.SetActive(counter == 1);
        confirmButton.gameObject.SetActive(counter == 1);
        policyToggle.gameObject.SetActive(counter == 1);
        privacyPolicyText.gameObject.SetActive(counter == 1);

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
        policyToggle.gameObject.SetActive(true);
        privacyPolicyText.gameObject.SetActive(true);

        emailInputField.text = "";
        firstNameInputField.text = "";
        lastNameInputField.text = "";
        counter = 1;
        UpdateUI();
    }

    public static class EmailTemplate
    {
        public static AlternateView GetHtmlBody(string firstName, string lastName, string gifPath)
        {
            string logoPath = "Assets/Logos/CofC.png";
            LinkedResource logo = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg)
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
                    <p style='font-size: 16px;'>Don’t forget to tag us with <strong>#CougarPride</strong> when you share your GIF.</p>
                </div>
            </body>";

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
            avHtml.LinkedResources.Add(logo);
            return avHtml;
        }
    }
}
