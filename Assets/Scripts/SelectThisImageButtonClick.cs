using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectThisImageButtonClick : MonoBehaviour
{
    public GameObject ImageTexture;

    public void SelectButtonClick()
    {
        Sprite selectedImage = textureToSprite(GameObject.Find("BigImage").GetComponent<RawImage>().texture as Texture2D);
        ImageTexture.GetComponent<Image>().sprite = selectedImage;

        DontDestroyOnLoad(ImageTexture);
        //SceneManager.LoadScene("07_AR");
        SceneManager.LoadScene("08_ARTest2");


    }

    Sprite textureToSprite(Texture2D _texture)
    {
        Rect rect = new Rect(0, 0, _texture.width, _texture.height);
        return Sprite.Create(_texture, rect, new Vector2(0.5f, 0.5f));

    }
}
