using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NewMonoBehaviourScript : MonoBehaviour
 

{
  // public Button button1;
    public SpriteRenderer square;


    //public Renderer square;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //square = GetComponent<Renderer>();
       // button1 = GetComponent<Button>();
      // button1.clicked += OnClick;
        square = GetComponent<SpriteRenderer>();
        square.enabled = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        //square.enabled = !square.isVisible;
        
    }
  public void switchBool () {
           square.enabled = !square.isVisible;
       }
    //void OnClick() {

    // square.enabled = !square.enabled;
    //}

    // helpful! https://vionixstudio.com/2022/05/21/making-a-simple-button-in-unity/
}
