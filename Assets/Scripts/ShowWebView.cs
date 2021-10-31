using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWebView : MonoBehaviour
{
    private WebViewObject webViewObject;
    public GameObject backButton;
    public GameObject cavas;

    // Use this for initialization
    void Start()
    {
        StartWebView();
        backButton.transform.SetAsLastSibling();
        cavas.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(webViewObject);
                return;
            }
        }
    }

    public void StartWebView()
    {
        // 로그인 페이지로 이어지게 수정
        //string strUrl = "http://220.116.166.15:8000/articles/list/";
        string strUrl = "http://192.168.0.13:8000/articles/list/";

        //테스트 페이지
        //string strUrl = "http://www.duksung.ac.kr/";

        webViewObject =
            (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) => {
            Debug.Log(string.Format("CallFromJS[{0}]", msg));
        });

        webViewObject.LoadURL(strUrl);
        webViewObject.SetVisibility(true);
        webViewObject.SetMargins(0, 70, 0, 0);
    }
}
