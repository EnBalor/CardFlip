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
        PlayerPrefs.SetString("StageLevel", bntTxt.text); 

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