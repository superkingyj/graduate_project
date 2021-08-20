using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

internal class SelectMenu : MonoBehaviour
{
    [SerializeField]
    public Button pickImage, uploadImage, gallery, community;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickImageClick()
    {
        SceneManager.LoadScene("03_PickImage");
    }

    public void uploadImageClick()
    {
        var pickImage = new PickUserImage();
        string pickedImageName = pickImage.PickImage(512);
        Debug.Log("저장된 이미지 이름 : " + pickedImageName);

        // save selected Image Name in to the scene object <ImageName> 
        GameObject.Find("ImageName").GetComponent<ImageName>().setpickedImageName(pickedImageName);
        SceneManager.LoadScene("07_AR");
        DontDestroyOnLoad(GameObject.Find("ImageName"));
    }

    public void galleryClick()
    {
        SceneManager.LoadScene("05_Gallery");
    }

    public void communityClick()
    {
        SceneManager.LoadScene("06_Community");
    }
}
