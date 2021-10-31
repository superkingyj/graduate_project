using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using UnityEngine.SceneManagement;

public class RegisterUser : MonoBehaviour
{
    // 나중에 입력받는걸로 수정!
    public InputField email, password, userName;
    public Button LoginButton;

    IEnumerator Upload()
    {
        //DataPost();
        string domain = "http://220.116.166.15:8000/users/login/";
        WWWForm form = new WWWForm();
        //string email = "test@test.com";
        //string password = "test1234";
        //string username = "testUser";
        form.AddField("email", email.text);
        form.AddField("password", password.text);
        form.AddField("username", userName.text);
        Debug.Log("사용자 정보 가져옴");

        UnityWebRequest www = UnityWebRequest.Post(domain, form);
        yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError)
        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else 
        {
            Debug.Log("로그인 성공!");
            Debug.Log(www.downloadHandler.text);
            SceneManager.LoadScene("02_Menu");
        }
    }

    public void LoginButtonClick()
    {
        StartCoroutine(Upload());
    }

    
}