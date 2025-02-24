using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System.Runtime.InteropServices.WindowsRuntime;

public class buttonPress : MonoBehaviour

{
    public Renderer cube1;
    public Button button1;
    public bool buttonState;

    // Start is called before the first frame update
    void Start()
    {
        cube1 = GetComponent<Renderer>();
        button1 = GetComponent<Button>();
        buttonState = ClusterInput.GetButton("button1");
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonState == true) { 
            if (cube1.isVisible)
            {
                cube1.enabled = false;
            }
            else { cube1.enabled = true; }

        } }
    /* void OnButtonClick ()
    {
        if (cube1.isVisible)
        {
            cube1.enabled = false;
        }
        else { cube1.enabled = true; }
        
    } */
} 
