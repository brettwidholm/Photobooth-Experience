using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetScene : MonoBehaviour
{

    public PathGetter getter;
    
    public void RestartScene()
    {
        getter.deletePath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //getter.Awake();
    }
}

