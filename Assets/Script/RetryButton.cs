using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void Reset() //테스트용 리셋버튼
    {
        PlayerPrefs.DeleteKey("Easy");
        PlayerPrefs.DeleteKey("Normal");
        PlayerPrefs.DeleteKey("Hard");
        SceneManager.LoadScene("StageScene");
    }
}
