using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowBigImage : MonoBehaviour
{
    public GameObject bigImage;
    public GameObject bigImageBackground;
    //211024 수정
    public Sprite selectedSprite;
    public GameObject ImageTexture;

    public void showSpriteImage()
    {
        Debug.Log(EventSystem.current);

        GameObject _Image = EventSystem.current.currentSelectedGameObject;
        Debug.Log(_Image.name);

        selectedSprite = _Image.GetComponent<Image>().sprite;
        ImageTexture.GetComponent<Image>().sprite = selectedSprite;
        bigImage.GetComponent<RawImage>().texture = textureFromSprite(selectedSprite);

        bigImage.GetComponent<CanvasRenderer>().SetAlpha(100);
        bigImageBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0.85f);
        bigImageBackground.transform.SetAsLastSibling();
        bigImage.transform.SetAsLastSibling();
    }

    public void closeBigImage()
    {
        bigImage.GetComponent<CanvasRenderer>().SetAlpha(0);
        bigImageBackground.transform.SetAsFirstSibling();
        bigImage.transform.SetAsFirstSibling();
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }


    public void Start()
    {
        GameObject.Find("Images").GetComponent<SpriteRenderer>().sortingOrder = 2;
        GameObject.Find("BigImage").GetComponent<SpriteRenderer>().sortingOrder = 1;
        GameObject.Find("BigImageBackground").GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public void Update()
    {
        
    }
}
