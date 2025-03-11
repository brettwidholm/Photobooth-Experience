using UnityEngine;
using System.Collections;
using TMPro;

//thank you https://discussions.unity.com/t/simple-timer/56201/2 :)

public class Timer: MonoBehaviour {

public float programTime = 20.0f;
public TextMeshProUGUI timerText;

public TextMeshProUGUI warningText; 

public void Start(){
    Debug.Log("GO GO GO!!!!");
    timerText.text = $"Timer: {programTime:F0}"; //no decimals, that creates more panic than needed
    warningText.text = "TOUCH THE SCREEN";
    timerText.enabled = false;
    warningText.enabled = false;
    //warning.HideWarning();
    Update();
}

public void Update(){
    
Debug.Log("updating timer");
//Console.WriteLine("updating timer");


timerText.text = $"Timer: {programTime:F0}";

if (Input.GetMouseButtonDown(0)){
    programTime = 20.0f; //if left click made reset timer 

    //in this branch we can add a circle that pops up to show a click (might be nice)
}

if(State.GetState() != 0 && State.GetState() != 3  ){

    programTime = programTime - Time.deltaTime;
}
else if(State.GetState() == 0 || State.GetState() == 3) {
    programTime = 20.0f;

}

if (programTime <= 0.0f)
{
   Debug.Log("out of time... bye :(");
   //Console.WriteLine("out of time... bye :(");
   State.StateReset();
   //TimerPause();
}

if (programTime <= 5.0f){
    
    timerText.enabled = true;
    warningText.enabled = true;
   // warning.ShowWarning();
}
else if(programTime > 5.0f){
    
    timerText.enabled = false;  
    warningText.enabled = false;
   // warning.HideWarning(); 
}



}




}
