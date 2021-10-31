using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUserImage : MonoBehaviour
{
    internal Texture2D texture;
    internal string imageName = null;

    public string PickImage(int maxSize)
    {

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
            }

            Debug.Log("받아온 이미지 : " + texture + texture.height + texture.width + texture.streamingMipmaps);
            Debug.Log("선택한 이미지 읽을 수 있는 이미지로 작업 시작");

            RenderTexture renderTexture = RenderTexture.GetTemporary(
                    texture.width,
                    texture.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

            Graphics.Blit(texture, renderTexture);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;

            Texture2D readableTextur2D = new Texture2D(texture.width, texture.height);
            readableTextur2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            readableTextur2D.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);

            Debug.Log("이미지 저장 작업 시작");
            byte[] bytes = readableTextur2D.EncodeToPNG();
            var dirPath = Application.persistentDataPath + "/UserImages/";
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            long time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            imageName = time.ToString()+".png";
            System.IO.File.WriteAllBytes(dirPath + imageName, bytes);
            Debug.Log("저장된 이미지 이름 : " + dirPath + imageName);
            var getPickedImage = new GetPickedImage();
            Debug.Log("getPickedImage update 호출");
            getPickedImage.Update();

            GameObject.Find("ImageName").GetComponent<ImageName>().setpickedImageName(imageName);
            DontDestroyOnLoad(GameObject.Find("ImageName"));
            Debug.Log("이미지 처리 다했고 이제 AR씬 불러옵니다~!");
            SceneManager.LoadScene("08_ARTest2");
        });
        return imageName;
    }

}