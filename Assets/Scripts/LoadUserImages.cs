using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUserImages : MonoBehaviour
{
    //private string folderpath = Application.persistentDataPath + "/Gallery/";
    public Texture2D defaultImage;
    private Sprite[] gameObj;
    private Texture2D[] textList;

    private string[] files;
    private string pathPreFix;

    public Button nextImages;
    public Button prveImages;
    private int index;

    void Start()
    {

        string path = Application.persistentDataPath + "/Gallery/";
        pathPreFix = @"file://";
        files = System.IO.Directory.GetFiles(path, "*.png");

        _LoadImages();

    }

    void Update()
    {

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

    private void  _LoadImages()
    {
        textList = new Texture2D[files.Length];

        int i = 0;
        int _index = 0;

        Debug.Log("i: " + i + "/ index: " + index);
        Debug.Log(files.Length);

        foreach (string tstring in files)
        {

            if (_index < index)
            {
                _index++;
                continue;
            }

            string pathTemp = pathPreFix + tstring;
            WWW www = new WWW(pathTemp);

            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);

            Rect rect = new Rect(0, 0, texTmp.width, texTmp.height);
            transform.GetChild(i).GetComponent<Image>().sprite = Sprite.Create(texTmp, rect, new Vector2(0.5f, 0.5f));
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
            for (int j = i; j<24; j++)
            {
                transform.GetChild(j).GetComponent<Image>().sprite = Sprite.Create(defaultImage, rect, new Vector2(0.5f, 0.5f));
            }
        }
    }
}
