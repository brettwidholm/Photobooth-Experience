using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetScene : MonoBehaviour
{

    public PathGetter getter;
    public EmailController emailController;
    public ScreenControl screenControl;
    
/*     public void RestartScene()
    {
        getter.deletePath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //getter.Awake();
    }
} */

public void RestartScene()
{   
   
    screenControl.RunWithLoadingScreen(
        onComplete: () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex),
        onStart: () =>
        {
            // Step 1: Wipe all visible objects manually
            screenControl.devMode.SetActive(false);
            screenControl.screen0.SetActive(false);
            screenControl.screen1.SetActive(false);
            screenControl.screen2.SetActive(false);
            screenControl.screen3.SetActive(false);
            screenControl.screen4.SetActive(false);
            screenControl.screen5.SetActive(false);
            screenControl.screen6.SetActive(false);
            screenControl.screen7.SetActive(false);
            screenControl.gifPrev.SetActive(false);
            screenControl.websosa.SetActive(false);
            screenControl.flash.SetActive(false);
            screenControl.emailEntryBox.SetActive(false);
            screenControl.backButtonInstructions.SetActive(false);
            screenControl.backButtonInfo.SetActive(false);
            screenControl.backButtonConfirm.SetActive(false);
            screenControl.previewShareButton.SetActive(false);
            screenControl.sendEmailButton.SetActive(false);
            screenControl.nextButtonInfo.SetActive(false);

            // Step 2: Clear any saved files
            getter.deletePath();
        },
        delay: 3.0f
    );
}


}
