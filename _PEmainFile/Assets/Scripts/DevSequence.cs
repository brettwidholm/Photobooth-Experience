using System;
using UnityEngine;
using UnityEngine.UI;

public class DevSequence : MonoBehaviour
{
    public ScreenControl screenControl;

    public Button topRight;
    public Button topLeft;
    public Button bottomLeft;
    public Button bottomRight; 

    private readonly string[] correctSequence = {"topRight", "topLeft", "bottomLeft", "bottomRight"};
    private string[] userSequence = new string[4];
    private int currentIndex = 0;
    void Start()
    {
        topRight.onClick.AddListener(() => AddToSequence("topRight"));
        topLeft.onClick.AddListener(() => AddToSequence("topLeft"));
        bottomLeft.onClick.AddListener(() => AddToSequence("bottomLeft"));
        bottomRight.onClick.AddListener(() => AddToSequence("bottomRight"));
    }

    void AddToSequence(string buttonName)
    {
        if (currentIndex < userSequence.Length)
        {
            userSequence[currentIndex] = buttonName;
            Debug.Log($"{buttonName} pressed. Current sequence = {string.Join(", ", userSequence)}");
            currentIndex++;

            if (currentIndex == userSequence.Length)
            {
                CheckSequence();
            }
        }
    }

    void CheckSequence()
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (userSequence[i] != correctSequence[i])
            {
                Debug.Log("Incorrect sequence. Start again.");
                ResetSequence();
                return;
            }
        }

        Debug.Log("Correct sequence! calling ShowDevMode()");
        screenControl.ShowDevMode();
        ResetSequence();
    }

    void ResetSequence()
    {
        Array.Clear(userSequence, 0, userSequence.Length);
        currentIndex = 0;
    }
}