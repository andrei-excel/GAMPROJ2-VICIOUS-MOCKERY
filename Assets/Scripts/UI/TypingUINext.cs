using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingUINext : MonoBehaviour
{
   
    [SerializeField] private TextMeshProUGUI wordNext;
    [SerializeField] private FlipPage pageFlip;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;

    bool hasActiveWord;

    private void Start()
    {
        wordToType = wordNext.text.ToLower();
        wordContainer = wordToType;
        typeIndex = 0;
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            typeIndex++;

            wordNext.text = wordNext.text.Remove(0, 1);
            wordNext.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            wordNext.text = wordContainer;
            wordNext.color = Color.green;
        }

        if (typeIndex >= wordToType.Length)
        {
            Debug.Log("NEXT PAGE");
            pageFlip.turnOnePageBtn_Click(FlipPage.ButtonType.NextButton);
        }
    }
}