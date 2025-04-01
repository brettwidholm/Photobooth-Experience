using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.Collections;

//note, thank chat for this one :)
public class GifGen : MonoBehaviour
{
    public string inputFolder =  Path.Combine(Application.dataPath, "Photos");
    public string outputFolder =  Path.Combine(Application.dataPath, "gif");
    public string outputGifName = "rad.gif";
    public int frameRate = 2; 

public IEnumerator DelayedGifCreation()
{
    yield return new WaitForSeconds(2f); // Wait for 2 seconds
    ConvertImagesToGif();
}

    public void ConvertImagesToGif()
    {
        if (!Directory.Exists(inputFolder))
        {
            UnityEngine.Debug.LogError("Input folder does not exist!");
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string ffmpegPath = @"C:\ffmpeg\bin\ffmpeg.exe";
        string framePattern = Path.Combine(inputFolder, "photo%0d.png"); // Adjust pattern based on file names
        string outputGifPath = Path.Combine(outputFolder, outputGifName);

        string arguments = $"-framerate {frameRate} -i \"{framePattern}\" -vf \"scale=640:-1:flags=lanczos\" -y \"{outputGifPath}\"";

        RunFFmpegCommand(ffmpegPath, arguments);

        UnityEngine.Debug.Log($"GIF saved to: {outputGifPath}");
    }

    public void RunFFmpegCommand(string ffmpegPath, string arguments)
    {
        Process process = new Process();
        process.StartInfo.FileName = ffmpegPath;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (!string.IsNullOrEmpty(error))
            UnityEngine.Debug.LogError("FFmpeg Error: " + error);
        else
            UnityEngine.Debug.Log("FFmpeg Output: " + output);
    }
}
