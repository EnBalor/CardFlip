using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public Text bntTxt;
    public void ChooseStage()
    {
        PlayerPrefs.SetString("StageLevel", bntTxt.text); // StageLevel에 버튼의 텍스트 저장(Easy, Normal, Hard)

        // 난이도를 정수로 저장(1, 2, 3)
        if (PlayerPrefs.GetString("StageLevel") == "Easy")
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        else if (PlayerPrefs.GetString("StageLevel") == "Normal")
        {
            PlayerPrefs.SetInt("Difficulty", 2);
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", 3);
        }

        SceneManager.LoadScene("MainScene");
    }
}