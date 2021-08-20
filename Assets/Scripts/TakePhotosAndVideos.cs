using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakePhotosAndVideos : MonoBehaviour
{

    public void backButtonClick()
    {
        SceneManager.LoadScene("02_Menu");
    }


    public void TakeScreenShot()
    {
        Debug.Log("사진 찍기 버튼 클릭");
        hideGUI(true);
        StartCoroutine(TakeScreenshotAndSave());
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
        var dirPath = Application.persistentDataPath + "/Gallery/";
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
        hideGUI(false);
    }

    // 스크린샷 찍을 때 GUI 버튼 및 Text를 없애주는 함수
    void hideGUI(bool removeGUI)
    {
        if (removeGUI)
        {
            GameObject.Find("Back").GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
            GameObject.Find("Photo").GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
            GameObject.Find("Video").GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
            GameObject.Find("Text").GetComponent<Text>().color = Color.clear;
        }
        else
        {
            GameObject.Find("Back").GetComponentInChildren<CanvasRenderer>().SetAlpha(100);
            GameObject.Find("Photo").GetComponentInChildren<CanvasRenderer>().SetAlpha(100);
            GameObject.Find("Video").GetComponentInChildren<CanvasRenderer>().SetAlpha(100);
            GameObject.Find("Text").GetComponent<Text>().color = Color.black;
        }
    }

    // 추후 시간이 남으면 구현 예정 .. 일단은 pass
    public void RecordeVideos()
    {
        //var video = GameObject.Find("ARCamera").GetComponent<ScreenRecorder>();
        //video.StartToRecord();
        return;
    }

}
