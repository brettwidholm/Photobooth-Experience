using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetScene : MonoBehaviour
{
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

