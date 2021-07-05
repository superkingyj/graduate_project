using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImages : MonoBehaviour
{
    public Image TestImage;
    private Sprite TestSprite;

    public void Start()
    {
        ChangeImage();
    }

    public void Update()
    {

    }

    public void ChangeImage()
    {
        Debug.Log("TestImage : " + TestImage);
        TestSprite = Resources.Load<Sprite>("Images/exImage_00");
        Debug.Log("TestSpirte : "+ TestSprite);
        TestImage.sprite = TestSprite;
    }
}
