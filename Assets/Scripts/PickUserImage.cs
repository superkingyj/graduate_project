using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUserImage : MonoBehaviour
{

    internal Texture2D texture;
    internal string imageName;

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
            Debug.Log("위치 : " + dirPath);
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            long time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            imageName = time.ToString()+".png";
            Debug.Log(imageName);
            //System.IO.File.WriteAllBytes(dirPath + UnityEngine.Random.Range(0, 100000) + ".png", bytes);
            System.IO.File.WriteAllBytes(dirPath + imageName, bytes);
            Debug.Log("저장된 이미지 이름 : " + dirPath + imageName);
        });

        return imageName;
    }
}

//internal class MakeReadableImage
//{
//    internal Texture2D createReadabeTexture2D(Texture2D texture2d)
//    {
//        RenderTexture renderTexture = RenderTexture.GetTemporary(
//                    texture2d.width,
//                    texture2d.height,
//                    0,
//                    RenderTextureFormat.Default,
//                    RenderTextureReadWrite.Linear);

//        Graphics.Blit(texture2d, renderTexture);
//        RenderTexture previous = RenderTexture.active;
//        RenderTexture.active = renderTexture;

//        Texture2D readableTextur2D = new Texture2D(texture2d.width, texture2d.height);
//        readableTextur2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
//        readableTextur2D.Apply();
//        RenderTexture.active = previous;
//        RenderTexture.ReleaseTemporary(renderTexture);

//        return readableTextur2D;

//    }
//}

//internal class SaveImageToLocalFolder
//{
//    internal void saving(byte[] bytes)
//    {
//        Debug.Log("사용자 사진을 넘겨 받았음");
//        var dirPath = Application.persistentDataPath + "/UserImages/";
//        Debug.Log("위치 : " + dirPath);
//        if (!System.IO.Directory.Exists(dirPath))
//        {
//            System.IO.Directory.CreateDirectory(dirPath);
//        }
//        System.IO.File.WriteAllBytes(dirPath + UnityEngine.Random.Range(0, 100000) + ".png", bytes);
//    }
    
//}

