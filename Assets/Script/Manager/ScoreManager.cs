using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public static int Score = 0;

    private UILabel UILabel_PointText;

    public void Awake()
    {
        UILabel_PointText = GameObject.Find("Point").GetComponent<UILabel>();
        Score = 0;
    }

    public void FixedUpdate()
    {
        UILabel_PointText.text = Score.ToString();
    }

    public static void SetScore(int value)
    {
        Score += value;
    }

    public static int GetScore()
    {
        return Score;
    }
}
