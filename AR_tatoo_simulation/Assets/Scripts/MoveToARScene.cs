using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToARScene : MonoBehaviour
{
    //private Touch tempTouchs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("터치");
            SceneManager.LoadScene("07_AR");

        }
    }

    private void OnMouseDown()
    {
        Debug.Log("터치");
        SceneManager.LoadScene("07_AR");
    }
}
