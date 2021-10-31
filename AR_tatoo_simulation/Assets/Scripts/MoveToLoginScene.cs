using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class MoveToLoginScene : MonoBehaviour
{

    public float delayTime = 3;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);

        Application.LoadLevel("01_Login");
    }
}
