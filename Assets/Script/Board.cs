using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Board : MonoBehaviour
{
    public GameObject card;

    public int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

    // Start is called before the first frame update
    void Start()
    {
        // 난이도(1,2,3) 값으로 배열 크기 조정
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

        StartCoroutine(CardAppear());
    }

    private IEnumerator CardAppear()
    {

        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);

            go.GetComponent<Card>().Setting(arr[i]);

            GameManager.instance.cardCount++;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
