using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.PhotoModule;
using OpenCVForUnity.UnityUtils;

public class InpaintMarker : MonoBehaviour

{
    //public MeshRenderer targetRenderer = 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inpaint(Renderer targetRenderer)
    {
        /////////////////////src/////////////////////
        //Texture2D srcTexture = Resources.Load("face") as Texture2D;
        byte[] bytes;

        string path = "/Editor/Vuforia/CylinderTargetTextures/CylinderTarget/211005.Body_scaled.jpg";
        bytes = System.IO.File.ReadAllBytes(Application.dataPath + path);
        Texture2D srcTexture = new Texture2D(500, 500);
        srcTexture.LoadImage(bytes);

        Mat srcMat = new Mat(srcTexture.height, srcTexture.width, CvType.CV_8UC3);

        Utils.texture2DToMat(srcTexture, srcMat);
        Debug.Log("srcMat.ToString() " + srcMat.ToString());

        /////////////////////mask/////////////////////
        //Texture2D maskTexture = Resources.Load("face_inpaint_mask") as Texture2D;
        path = "/OpenCVForUnity/Examples/Resources/face_inpaint_mask.png";
        bytes = System.IO.File.ReadAllBytes(Application.dataPath + path);
        Texture2D maskTexture = new Texture2D(500, 500);
        maskTexture.LoadImage(bytes);
        Mat maskMat = new Mat(maskTexture.height, maskTexture.width, CvType.CV_8UC1);

        Utils.texture2DToMat(maskTexture, maskMat);
        Debug.Log("maskMat.ToString() " + maskMat.ToString());

        /////////////////////dst/////////////////////
        Mat dstMat = new Mat(srcMat.rows(), srcMat.cols(), CvType.CV_8UC3);

        Photo.inpaint(srcMat, maskMat, dstMat, 0, Photo.INPAINT_NS);
        Texture2D texture = new Texture2D(dstMat.cols(), dstMat.rows(), TextureFormat.RGBA32, false);

        Utils.matToTexture2D(dstMat, texture);
        Debug.Log(targetRenderer);
        targetRenderer.materials[1].mainTexture = texture;

    }
}
