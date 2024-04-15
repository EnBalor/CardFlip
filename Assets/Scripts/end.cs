using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
