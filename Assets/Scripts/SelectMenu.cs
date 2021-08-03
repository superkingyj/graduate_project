using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectMenu : MonoBehaviour
{
    public Button pickImage, uploadImage, gallary, community;

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
        //SceneManager.LoadScene("04_UploadImage");
        // 네이티브 갤러리에서 사용자 사진 불러오기
        var pickImage = new PickUserImage();
        Texture2D userImage = pickImage.PickImage(512);
        //Debug.Log(userImage);
        //Debug.Log("네이티브 갤러리에서 사용자 사진 불러오기 성공");
        SceneManager.LoadScene("07_AR");
    }

    public void gallaryClick()
    {
        SceneManager.LoadScene("05_Gallary");
    }

    public void communityClick()
    {
        SceneManager.LoadScene("06_Community");
    }
}
