using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BtnManager : MonoBehaviour {

    public static bool PauseCheck = false;
    public static bool GameOverCheck = false;
    public static bool FirstCheck = false;

    public bool OneClick;

    public GameObject PBG;

    public static Color UI_BIB_1;
    public static Color UI_BIB_2;

    public UISprite BIB_Image;

    float UI_BIBDuration = 0.4f;
    float UI_BIBSmoothness = 0.02f;

    public void Awake()
    {
        PBG = GameObject.Find("pbg");
        BIB_Image = GameObject.Find("QuitImage").GetComponent<UISprite>();
        OneClick = false;
        PauseCheck = false;
    }

    public void Start()
    {
        PBG.SetActive(false);
        UI_BIB_1 = new Color32(255, 255, 255, 255);
        UI_BIB_2 = new Color32(255, 255, 255, 0);
        StartCoroutine("ChangeColorWhite");
    }

    public void Update()
    {
        if(GameOverCheck == true && FirstCheck == false)
        {
            StartCoroutine("ChangeColorBlack");
            GameObject.Find("Player").SetActive(false);
            FirstCheck = true;
        }
    }

    public void PauseBtn() //일시정지 버튼
    {
        if (PauseCheck == false)
        {
            PauseCheck = true;
            Time.timeScale = 0;
            PBG.SetActive(true);
        }
        else
        {
            PauseCheck = false;
            Time.timeScale = 1;
            PBG.SetActive(false);
        }
    }

    public void QuitBtn() //종료 버튼
    {
        Application.Quit();
        Time.timeScale = 1;
        StartCoroutine("ChangeColorBlack");
    }

    public void StartBtn() //메인 씬 버튼
    {
        if (OneClick == false)
        {
            StartCoroutine("ChangeColorBlack");
            Invoke("MainSceenLoader", 2f);
            OneClick = true;
        }
    }

    public void GotoStartSceenBtn() //스타트 씬 으로 가는 void문
    {
        if (OneClick == false)
        {
            Time.timeScale = 1;
            StartCoroutine("ChangeColorBlack");
            Invoke("StartSceenLoader", 2f);
            OneClick = true;
        }
    }

    public void MainSceenLoader()
    {
        SceneManager.LoadScene("NEW_Main");
    }

     public void StartSceenLoader()
    {
        SceneManager.LoadScene("Start");
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
