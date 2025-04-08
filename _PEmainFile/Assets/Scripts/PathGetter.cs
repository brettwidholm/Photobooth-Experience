using UnityEngine;using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.Collections;
public class PathGetter : MonoBehaviour
{

    public string inputFolder;
    private string photo;
    private string gif;
    public string folderPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        inputFolder = Application.persistentDataPath;   
        folderPath = Path.Combine(inputFolder, "Photos");
        photo = folderPath;
        Directory.CreateDirectory(folderPath);
        folderPath = Path.Combine(inputFolder, "gif");
        gif = folderPath;
        Directory.CreateDirectory(folderPath);
    }

    // Update is called once per frame
    public string getPath()
    {
        return inputFolder;
    }

    
    public void deletePath()
    {
         
        if (Directory.Exists(photo))
        {
            Directory.Delete(photo, true); 
        }   

        if (Directory.Exists(gif))
        {
            Directory.Delete(gif, true); 
        }   
    }
}
