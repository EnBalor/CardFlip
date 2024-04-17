using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    public SpriteRenderer frontImage;
    AudioSource audioSource;

    public AudioClip flipSound;

    bool isFlipedBefore = false;

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("card_flip"))
        {
            transform.Find("Front").gameObject.SetActive(false);
            transform.Find("Back").gameObject.SetActive(true);
        }
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {
        if(GameManager.instance.isStart == true)
        {
            audioSource.PlayOneShot(flipSound);
            anim.SetBool("isOpen", true);
            front.SetActive(true);
            back.SetActive(false);

            if (GameManager.instance.firstCard == null)
            {
                GameManager.instance.firstCard = this;
            }

            else
            {
                GameManager.instance.secondCard = this;
                GameManager.instance.Matched();
                GameManager.instance.openCount++;
            }

            if (!isFlipedBefore)
            {
                isFlipedBefore = true;
                SpriteRenderer spriteRenderer = back.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0.9f, 0.9f, 0.9f, 1);
            }
        }
    }



    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
