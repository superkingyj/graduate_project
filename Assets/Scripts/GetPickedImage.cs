using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GetPickedImage : MonoBehaviour
{

    [SerializeField]
    private GameObject TestPlane;

    [SerializeField]
    private Texture2D ChangedTexture;

    [SerializeField]
    private Texture2D Texture;

    [SerializeField]
    private byte[] bytes;

    [SerializeField]
    private Renderer TestPlaneRenderer;

    [SerializeField]
    internal string pickedImageName;

    private void Start()
    {
        // 사용자가 선택한 이미지 불러오기
        // 테스트용 이미지 불러온것... 후에 밑에 주석 해제
        string filePath = Application.persistentDataPath + "/UserImages/" + "1628835330.png";
        //string filePath = Application.persistentDataPath + "/UserImages/" + GameObject.Find("ImageName").GetComponent<ImageName>().getPickedImageName();

        Debug.Log(filePath);

        new WaitForSeconds(5);

        if (string.IsNullOrEmpty(filePath))
        {
            Debug.Log("파일 없음..");
            return;
        }
        if (System.IO.File.Exists(filePath)){
            bytes = System.IO.File.ReadAllBytes(filePath);
            Debug.Log(bytes.Length);
            Texture = new Texture2D(500, 500);
            Texture.LoadImage(bytes);
            TestPlaneRenderer.material.mainTexture = Texture;
        }
    }

    private Texture2D FlipTexture(Texture2D original, bool upSideDown = true)
    {

        Texture2D flipped = new Texture2D(original.width, original.height);

        int xN = original.width;
        int yN = original.height;


        for (int i = 0; i < xN; i++)
        {
            for (int j = 0; j < yN; j++)
            {
                if (upSideDown)
                {
                    flipped.SetPixel(j, xN - i - 1, original.GetPixel(j, i));
                }
                else
                {
                    flipped.SetPixel(xN - i - 1, j, original.GetPixel(i, j));
                }
            }
        }
        flipped.Apply();

        return flipped;
    }
}

