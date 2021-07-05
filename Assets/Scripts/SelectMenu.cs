using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("04_UploadImage");
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
