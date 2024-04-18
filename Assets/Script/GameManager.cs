using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;
    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public Text nowTime;
    public Text bestTime;
    public Text openCountTxt;
    public Text scoreTxt;
    public Text NameTxt;


    public GameObject endTxt;
    public GameObject increaseTime;

    public int cardCount = 0;
    public int openCount = 0;
    public float time = 0f;
    float score;

    public Animator animator;
    AudioSource audioSource;

    public AudioClip flipSound;
    public AudioClip matchedSound;
    public AudioClip unMatchedSound;
    public AudioClip gameStartSound;

    public bool isStart = false;

    readonly string easyKey = "easyBest";
    readonly string normalKey = "normalBest";
    readonly string hardKey = "hardKey";

    float timeAfterFirstCardFlip = 0.0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
    }

    private string GetKey()
    {
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 1:
                return easyKey;
            case 2:
                return normalKey;
            default:
                return hardKey;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        
        bestTime.text = PlayerPrefs.GetFloat(GetKey()).ToString("N2");

        time = 30f;
        score = 100f;
        openCount = 0;

        audioSource.PlayOneShot(gameStartSound);
    }

    // Update is called once per frame
    void Update()
    {
        if(cardCount == board.arr.Length)
        {
            isStart = true;
            animator.SetBool("TxtAnimStart", true);
        }

        if(isStart == true)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            openCountTxt.text = openCount.ToString();
        }

        if(time <= 0.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            time = 10.0f;
        }

        if(time <= 10.0f)
        {
            animator.SetBool("AlertTxt", true);

            AudioManager.instance.audioSource.pitch = 1.4f;
        }

        if (firstCard)
        {
            timeAfterFirstCardFlip -= Time.deltaTime;
            if (timeAfterFirstCardFlip <= 0)
            {
                firstCard.GetComponent<Card>().CloseCard();
                SetTimeAfterFirstCardFlip(0.0f);
                firstCard = null;
            }
        }
    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchedSound);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            SettingNameTxt();
            NameTxt.color = Color.white;

            if (cardCount == 0)
            {
                BestTime();
                endTxt.SetActive(true);
                score = time / (1 + openCount) * 100;
                scoreTxt.text = score.ToString("N2");

                if (PlayerPrefs.GetString("StageLevel") == "Easy")
                {
                    PlayerPrefs.SetInt("Easy", 1);
                }
                else if (PlayerPrefs.GetString("StageLevel") == "Normal")
                {
                    PlayerPrefs.SetInt("Normal", 1);
                }

                Time.timeScale = 0.0f;
            }
        }

        else
        {
            NameTxt.text = "실패!";
            NameTxt.color = Color.red;
            time -= 1f;
            increaseTime.SetActive(true);
            audioSource.PlayOneShot(unMatchedSound);
            Invoke("TxtControl", 0.5f);
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void TxtControl()
    {
        increaseTime.SetActive(false);
    }

    private void BestTime()
    {
        nowTime.text = time.ToString("N2");
        string key = GetKey();

        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetFloat(key) < time)
            {
                PlayerPrefs.SetFloat(key, time);
                bestTime.text = time.ToString("N2");
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            bestTime.text = time.ToString("N2");
        }
    }

    void SettingNameTxt()
    {
        switch (firstCard.idx)
        {
            case 0: case 1:
                NameTxt.text = "김창연";
                break;
            case 2: case 3:
                NameTxt.text = "박성준";
                break;
            case 4: case 5:
                NameTxt.text = "박신환";
                break;
            case 6: case 7:
                NameTxt.text = "이서영";
                break;
            case 8: case 9:
                NameTxt.text = "윤정빈";
                break;
        }
    }
    public void SetTimeAfterFirstCardFlip(float second)
    {
        timeAfterFirstCardFlip = second;
    }
}

