using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LoadServiceImages : MonoBehaviour
{
    public Texture2D defaultImage;
    private Texture[] files  = new Texture2D[25];

    public Button nextImages;
    public Button prveImages;
    public Texture errorImage;
    private int index;

    // 유진 자취방
    //private string domain = "http://220.116.166.15:8000/media/image/";

    // 로컬 서버
    //private string domain = "http://127.0.0.1:8000/media/image/";

    // 하나누라관 서버
    private string domain = "http://192.168.0.13:8000/media/image/";

    void Start()
    {
        IEnumerator GetTexture()
        { 
            for (int i = 0; i < 25; i++)
            {
                int imageIndex = i+1;
                int fileIndex = i;
                string number = imageIndex.ToString("D3");
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(String.Format("{0}image_{1}.png", domain, number));
                Debug.Log(String.Format("{0}image_{1}.png", domain, number));
                yield return www.SendWebRequest();

                if (www.error == null)
                {
                    Texture downloadImage = DownloadHandlerTexture.GetContent(www);
                    files[fileIndex] = downloadImage;
                    Debug.Log(imageIndex + "번째 이미지 서비스 완료");
                }
                else
                {
                    Debug.Log("이미지를 다운로드 받을 수 없습니다. 에러내용 : " + www.error);
                    yield break;
                }
            }
            
        }

        IEnumerator waitUntilDownloadImages()
        {

            yield return StartCoroutine(GetTexture());
            _LoadImages();

        }

        StartCoroutine(waitUntilDownloadImages());

    }

    public void nextImagesButtonClick()
    {
        Debug.Log("다음 이미지 로드하기");
        _LoadImages();
    }

    public void prevImagesButtonClick()
    {
        Debug.Log("이전 이미지 로드하기");
        index -= 24;
        _LoadImages();
    }

    private void _LoadImages()
    {
        int i = 0;
        int _index = 0;

        Debug.Log("i: " + i + "/ index: " + index);
        Debug.Log(files.Length);

        foreach (Texture tstring in files)
        {

            if (_index < index)
            {
                _index++;
                continue;
            }

            Rect rect = new Rect(0, 0, tstring.width, tstring.height);
            transform.GetChild(i).GetComponent<Image>().sprite = Sprite.Create(tstring as Texture2D, rect, new Vector2(0.5f, 0.5f));
            i++;

            if (i >= 24)
            {
                index += 24;
                break;
            }
        }

        if (i >= 24)
        {
            return;
        }
        else
        {
            Rect rect = new Rect(0, 0, defaultImage.width, defaultImage.height);
            for (int j = i; j < 24; j++)
            {
                transform.GetChild(j).GetComponent<Image>().sprite = Sprite.Create(defaultImage, rect, new Vector2(0.5f, 0.5f));
            }
        }
    }
}
