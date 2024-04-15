using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    float myTime = 0.0f;
    public Text txtTime;

    public Text txtCountAfterFirstCard;
    const float MaxCountAfterFirstCard = 5.0f;
    float CountAfterFirstCard = 0.0f;

    public GameObject scoreBoard;
    bool isRunning = true;
    public Text txtbestScore;
    public Text txtmyScore;

    public GameObject cards;
    public GameObject card;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioSource audioSource;
    public AudioClip matchSound;

    public AudioClip unmatchSound;

    private void Awake()
    {
        GM = this;
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        scoreBoard.SetActive(false);

        int[] images = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        // order list randomly
        images = images.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = cards.transform;

            float posX = (i / 4) * 1.4f - 2.1f;
            float posY = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(posX, posY, 0);

            string rtanName = "rtan" + images[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    void Update()
    {
        if (isRunning)
        {
            myTime += Time.deltaTime;
            txtTime.text = myTime.ToString("N1");
            txtCountAfterFirstCard.text = CountAfterFirstCard.ToString("N1");
            if (myTime >= 60.0f)
            {
                GameOver();
            }
        }

        if (firstCard)
        {
            CountAfterFirstCard -= Time.deltaTime;
            if (CountAfterFirstCard <= 0)
            {
                firstCard.GetComponent<card>().closeCard();
                SetZeroCountAfterFirstCard();
                firstCard = null;
            }
        }
       
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(matchSound);

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int leftCard = cards.transform.childCount;
            if (leftCard == 2)
            {
                GameOver();
            }
        }
        else
        {
            audioSource.PlayOneShot(unmatchSound);

            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

    private void GameOver()
    {
        foreach (Transform child in cards.transform)
        {
            Destroy(child.gameObject);
        }
        isRunning = false;
        Time.timeScale = 0.0f;
        txtTime.text = null;

        txtmyScore.text = myTime.ToString("N1");

        if (PlayerPrefs.HasKey("bestScore") == false)
        {
            PlayerPrefs.SetFloat("bestScore", myTime);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore") > myTime)
            {
                PlayerPrefs.SetFloat("bestScore", myTime);
            }
        }
        txtbestScore.text = PlayerPrefs.GetFloat("bestScore").ToString("N1");

        scoreBoard.SetActive(true);
    }
    public void SetMaxCountAfterFirstCard()
    {
        CountAfterFirstCard = MaxCountAfterFirstCard;
    }
    public void SetZeroCountAfterFirstCard()
    {
        CountAfterFirstCard = 0;
    }
}
