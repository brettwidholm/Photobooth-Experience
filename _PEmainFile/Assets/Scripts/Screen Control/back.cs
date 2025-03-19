using UnityEngine;

public class BackControl : MonoBehaviour
{

    public ScreenControl screenControl;

    public GameObject back;
    void Start()
    {
        Update();
    }

    void Update()
    {
        if(screenControl.IsScreenActive("Photo Capture")){
            back.SetActive(false);
        }
        else{
            back.SetActive(true); 
        }


    }
}


//implement state tracker, set state method, in each showscreen() set state to respective int.  
//in BACK, if state = 2 ONLICK then state = 0 --> calls showscreen0

//backbutton goes at bottom of hierarchy