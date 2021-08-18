using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageName : MonoBehaviour
{

    public string pickedImageName;

    public void setpickedImageName(string pickedImageName)
    {
        this.pickedImageName = pickedImageName;
    }

    public string getPickedImageName()
    {
        return this.pickedImageName;
    }
}
