using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public Button NormalBtn;
    public Button HardBtn;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Easy") == 1)
        {
            /*
            ColorBlock colorBlock = NormalBtn.colors;
            colorBlock.normalColor = Color.white;
            NormalBtn.colors = colorBlock;
            */
            NormalBtn.interactable = true;
        }
        if (PlayerPrefs.GetInt("Normal") == 1)
        {
            HardBtn.interactable = true;
        }
    }
}
