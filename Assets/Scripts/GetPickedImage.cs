using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
//using System.Collections;
//using OpenCVForUnity.CoreModule;
//using OpenCVForUnity.PhotoModule;
//using OpenCVForUnity.UnityUtils;
using static InpaintMarker;

public class GetPickedImage : MonoBehaviour
{

    [SerializeField]
    private GameObject TestPlane;

    [SerializeField]
    private Texture2D ChangedTexture;

    [SerializeField]
    public Texture2D Texture;

    [SerializeField]
    private byte[] bytes;

    [SerializeField]
    public Renderer TestPlaneRenderer;

    [SerializeField]
    internal string pickedImageName;

    public bool flag = true;

    public void Start()
    {
        // 사용자가 선택한 이미지 불러오기
        // 테스트용 이미지 불러온것... 후에 밑에 주석 해제
        //string filePath = Application.persistentDataPath + "/UserImages/" + "image_014.png";
        //new WaitForSeconds(5);

        //if (string.IsNullOrEmpty(filePath))
        //{
        //    Debug.Log("파일 없음..");
        //    return;
        //}
        //if (System.IO.File.Exists(filePath))
        //{
        //    bytes = System.IO.File.ReadAllBytes(filePath);

        //    Debug.Log(bytes.Length);
        //    Texture = new Texture2D(500, 500);
        //    Debug.Log(Texture.GetType());
        //    Texture.LoadImage(bytes);

        //    // 211024 수정
        //    Texture2D tex = null;
        //    tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        //    for (int y = 0; y < tex.height; y++)
        //    {
        //        for (int x = 0; x < tex.width; x++)
        //        {
        //            Color color = Color.black;
        //            tex.SetPixel(x, y, color);
        //        }
        //    }
        //    tex.Apply();

        //    TestPlaneRenderer.material.mainTexture = Texture;
        //}

        //try (GameObject.Find("ImageName").GetComponent<ImageName>().getPickedImageName() != null)
        try // 사용자가 선택한 이미지를 가져오는 경우
        {
            string filePath = Application.persistentDataPath + "/UserImages/" + GameObject.Find("ImageName").GetComponent<ImageName>().getPickedImageName();
            Debug.Log(filePath);
            Debug.Log("사용자 이미지 선택 씬에서 넘어옴");

            new WaitForSeconds(5);

            if (string.IsNullOrEmpty(filePath))
            {
                Debug.Log("파일 없음..");
                return;
            }
            if (System.IO.File.Exists(filePath))
            {
                bytes = System.IO.File.ReadAllBytes(filePath);
                Debug.Log(bytes.Length);
                Texture = new Texture2D(500, 500);
                Texture.LoadImage(bytes);
                TestPlaneRenderer.material.mainTexture = Texture;
            }
        }
        catch // 03_PickImage Scene에서 선택한 이미지를 가져오는 경우
        {
            Debug.Log("03_PickImage Scene에서 넘어옴");
            Texture = textureFromSprite(GameObject.Find("ImageTexture").GetComponent<Image>().sprite);
            TestPlaneRenderer.material.mainTexture = Texture;
        }
    }

    public void Update()
    {
        if (flag && (GameObject.Find("ImageName").GetComponent<ImageName>().getPickedImageName() != null))
        {
            string filePath = Application.persistentDataPath + "/UserImages/" + GameObject.Find("ImageName").GetComponent<ImageName>().getPickedImageName();
            Debug.Log(filePath);
            Debug.Log("imageName 업데이트 후 변경");

            new WaitForSeconds(5);

            if (string.IsNullOrEmpty(filePath))
            {
                Debug.Log("파일 없음..");
                return;
            }
            if (System.IO.File.Exists(filePath))
            {
                bytes = System.IO.File.ReadAllBytes(filePath);
                Debug.Log(bytes.Length);
                Texture = new Texture2D(500, 500);
                Texture.LoadImage(bytes);
                TestPlaneRenderer.material.mainTexture = Texture;
            }

            flag = false;
        }
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }

    public Vector3 GetSpriteSize(GameObject _target)
    {
        Vector3 worldSize = Vector3.zero;
        if (_target.GetComponent<SpriteRenderer>())
        {
            Vector2 spriteSize = _target.GetComponent<SpriteRenderer>().sprite.rect.size;
            Vector2 localSpriteSize = spriteSize / _target.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            worldSize = localSpriteSize; worldSize.x *= _target.transform.lossyScale.x;
            worldSize.y *= _target.transform.lossyScale.y;
        }
        else
        {
            Debug.Log("SpriteRenderer Null");
        }
        return worldSize;
    }
}