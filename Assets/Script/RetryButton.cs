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

    public void Reset() //�׽�Ʈ�� ���¹�ư
    {
        PlayerPrefs.DeleteKey("Easy");
        PlayerPrefs.DeleteKey("Normal");
        PlayerPrefs.DeleteKey("Hard");
        SceneManager.LoadScene("StageScene");
    }
}
