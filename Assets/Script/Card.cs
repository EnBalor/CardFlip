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

    bool isFlipedOnce = false;
    const float MaxTimeAfterFirstCardFlip = 5.0f;
    const float ZeroTime = 0.0f;

    private void Update()
    {
        
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");   
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {
        if(GameManager.instance.isStart == true)
        {
            audioSource.PlayOneShot(flipSound);
            anim.SetBool("isOpen", true);

            if (GameManager.instance.firstCard == null)
            {
                GameManager.instance.firstCard = this;
                GameManager.instance.SetTimeAfterFirstCardFlip(MaxTimeAfterFirstCardFlip);
            }

            else
            {
                GameManager.instance.secondCard = this;
                GameManager.instance.SetTimeAfterFirstCardFlip(ZeroTime);
                GameManager.instance.Matched();
                GameManager.instance.openCount++;
            }

            if (!isFlipedOnce)
            {
                isFlipedOnce = true;
                SpriteRenderer spriteRenderer = back.GetComponent<SpriteRenderer>();
                //����ȭ�� �� �̹��ѵ�, ����� ���� �ǵ���ؼ� �����ϴ°ɷ�
                //�����Ҷ� ȿ���� �߰� �����غ���
                spriteRenderer.color = new Color(0.7f, 0.7f, 0.7f, 1);
            }
        }
    }


    //�κ�ũ ���� �Լ��� ���� �ڷ�ƾ �����ϴ� �ð� ������ ���� �� ����
    //�ڷ�ƾ���� �����ϴ� �۾� �ϳ��� �ϸ� ������
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
