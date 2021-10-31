using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopS : MonoBehaviour
{
    public SpriteRenderer ShopImage;

    public void Awake()
    {
        ShopImage.color = new Color(255, 255, 255, 0);
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            ShopImage.color = new Color32(255, 255, 255, 255);
        }
    }

     void OnTriggerExit2D(Collider2D other)
    {
        ShopImage.color = new Color32(255, 255, 255, 0);
    }
}
