using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.InteropServices;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject spawnPoint;

    GameObject go;

    public int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

    float speed = 5.0f;

    float cardPosX = 0f;
    float cardPosY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // ���̵�(1,2,3) ������ �迭 ũ�� ����
        int diff = PlayerPrefs.GetInt("Difficulty") * 4;
        Array.Resize(ref arr, diff + 4);

        for (int i = 0; i < arr.Length; i += 1)
        {
            int j = UnityEngine.Random.Range(i, arr.Length);
            if (i != j)
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        //���� �������� ī�带 �����ϱ� ���� �ڷ�ƾ ���
        StartCoroutine(CardAppear());
    }

    private void Update()
    {
        //go.transform.position = new Vector2(x, y);

        Vector3 cardSet = new Vector3(cardPosX, cardPosY, 0f);
        Vector3 dir = cardSet - spawnPoint.transform.position;

        Debug.Log(cardSet);

        go.transform.Translate(dir * speed * Time.deltaTime);

    }

    private IEnumerator CardAppear()
    {
        for (int i = 0; i < arr.Length; i++)
        {
            go = Instantiate(card, spawnPoint.transform);

            cardPosX = (i % 4) * 1.4f - 2.1f;
            cardPosY = (i / 4) * 1.4f - 3.0f;

            go.GetComponent<Card>().Setting(arr[i]);

            GameManager.instance.cardCount++;

            //ī�尡 �ѹ��� �������� �ʵ��� �ణ�� �ð� ������ ��
            yield return new WaitForSeconds(0.1f);
        }
    }
}
