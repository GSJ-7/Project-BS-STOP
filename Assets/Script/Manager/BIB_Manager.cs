using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIB_Manager : MonoBehaviour
{
    public Color UI_BIB_1;
    public Color UI_BIB_2;

    public UISprite BIB_Image;

    UILabel UILabel_DeathText;

    bool DeathCheck = false;

    float UI_BIBDuration = 0.4f;
    float UI_BIBSmoothness = 0.02f;

    public void Awake()
    {
        BIB_Image = GameObject.Find("BIB_Image").GetComponent<UISprite>();
        UILabel_DeathText = GameObject.Find("Death_Text").GetComponent<UILabel>();
    }
    public void Start()
    {
        UILabel_DeathText.color = UI_BIB_2;

        StartCoroutine("ChangeColorWhite");
    }
    
    public void Update()
    {
        if (PlayerManager.health <= 0 && DeathCheck == false)
        {
            StartCoroutine("ChangeColorBlack");

            GameObject.Find("Player").SetActive(false);
            DeathCheck = true;

            UILabel_DeathText.color = UI_BIB_1;
        }
    }

    IEnumerator ChangeColorBlack() //검은색 배경으로 바꿈
    {
        float progress = 0;
        float increment = UI_BIBSmoothness / UI_BIBDuration;
        
        while (progress < 1)
        {
            BIB_Image.color = Color.Lerp(UI_BIB_2, UI_BIB_1, progress);
            progress += increment;
            yield return new WaitForSeconds(UI_BIBSmoothness);
        }

        BIB_Image.color = UI_BIB_1;

        yield break;
    }

    IEnumerator ChangeColorWhite() //하얀색(알파값 0) 배경으로 바꿈
    {
        float progress = 0;
        float increment = UI_BIBSmoothness / UI_BIBDuration;

        while (progress < 1)
        {
            BIB_Image.color = Color.Lerp(UI_BIB_1, UI_BIB_2, progress);
            progress += increment;
            yield return new WaitForSeconds(UI_BIBSmoothness);
        }

        BIB_Image.color = UI_BIB_2;

        yield break;
    }
}
