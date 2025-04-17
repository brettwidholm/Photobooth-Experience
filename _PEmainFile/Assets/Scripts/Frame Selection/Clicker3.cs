using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class Clicker3 : MonoBehaviour, IPointerClickHandler
{
 //   public bool selected = false;
    public RawImage frame;

    public string frameName = "None";
    public int index = 0;

    public FrameLoaderUI fl;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("3 is clicked");
   //     selected = true;
        frame.color = Color.green;
        ClickChecker.setSelected(3);
        State.setState(index);
        fl.LoadNextImage();

        
    }

    
    public void Update(){
        if (ClickChecker.getSelected() != 3){
            frame.color = Color.white;
        }

    }

    public void LoadTexture(string path, int ind)
    {
        byte[] imageBytes = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        frame.texture = texture;

        frameName = path;
        index = ind;

    }

        public string getCurrentFrame(){
        if(index == 0){
            return "None";
        }
        return frameName;
    }


}