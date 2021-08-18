using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotosAndVideos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeScreenShot()
    {
        TakeScreenshotAndSave();
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D picture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        picture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        picture.Apply();

        Debug.Log(picture);

        // Save the screenshot to Gallery/Photos
        byte[] bytes = picture.EncodeToPNG();
        var dirPath = Application.persistentDataPath + "/Gallary/";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        long time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        string imageName = time.ToString() + ".png";

        Debug.Log(bytes);
        Debug.Log(dirPath + imageName);

        System.IO.File.WriteAllBytes(dirPath + imageName, bytes);

        Destroy(picture);
    }

    //구현 예정
    //private IEnumerator TakeVideosAndSave()
    //{

    //}

}
