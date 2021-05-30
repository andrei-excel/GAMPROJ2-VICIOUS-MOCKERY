using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TextMeshProUGUI text;

    private float speed;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;

    void Start()
    {
        speed = enemyData.Speed;
        typeIndex = 0;

        switch (enemyData.Type)
        {
            case EnemyType.Slow:
                wordToType = WordGenerator.GetHardWord();
                break;
            case EnemyType.Fast:
                wordToType = WordGenerator.GetEasyWord();
                break;
            case EnemyType.Normal:
                wordToType = WordGenerator.GetNormalWord();
                break;
        }

        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    public void TypeLetter(char letter)
    {
        if(EnemyWordInput.hasActiveWord == true)
        {
            if (wordToType[typeIndex] == letter)
            {
                typeIndex++;

                text.text = text.text.Remove(0, 1);
                text.color = Color.red;
            }
            else
            {
                typeIndex = 0;

                text.text = wordContainer;
                text.color = Color.green;
            }
        }
        else
        {
            if (wordToType[typeIndex] == letter)
            {
                EnemyWordInput.hasActiveWord = true;
                typeIndex++;

                text.text = text.text.Remove(0, 1);
                text.color = Color.red;
            }
        }

        if (typeIndex >= wordToType.Length)
        {
            EnemyWordInput.hasActiveWord = false;
            Destroy(gameObject);
        }
    }

    //TO DO: Should only have 1 active word at all times
}
