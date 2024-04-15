using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip flipSound;

    GameObject back;

    bool isFlipedBefore = false;
    void Start()
    {
        back = transform.Find("back").gameObject;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("card_flip"))
        {
            transform.Find("front").gameObject.SetActive(true);
            transform.Find("back").gameObject.SetActive(false);
        }
    }

    public void flipCard()
    {
        audioSource.PlayOneShot(flipSound);

        anim.SetBool("isOpen", true);

        if (GameManager.GM.firstCard == null)
        {
            GameManager.GM.firstCard = gameObject;
            GameManager.GM.SetMaxCountAfterFirstCard();
        }
        else
        {
            GameManager.GM.secondCard = gameObject;
            GameManager.GM.SetZeroCountAfterFirstCard();
            GameManager.GM.isMatched();
        }

        if (!isFlipedBefore)
        {
            isFlipedBefore = true;
            SpriteRenderer spriteRenderer = back.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0.9f, 0.9f, 0.9f, 1);
        }

    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.5f);
    }
    private void destroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void closeCard()
    {
        Invoke("closeCardInvoke", 0.5f);
    }
    private void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("front").gameObject.SetActive(false);
        transform.Find("back").gameObject.SetActive(true);
    }
}
