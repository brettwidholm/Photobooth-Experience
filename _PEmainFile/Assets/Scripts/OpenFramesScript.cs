using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.UI;

public class OpenFrames : MonoBehaviour
{
    public PathGetter getter;
    public string folderPath;


    public void Start(){
        folderPath = Path.Combine(getter.getPath(), "Frames");
    }
    public void openFolder(){
        Process.Start("explorer.exe", folderPath);

    }
}