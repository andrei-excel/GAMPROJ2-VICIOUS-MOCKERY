using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingUIStart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI word;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    public LevelChanger levelChanger;
    public TMP_Text TextComponent;

    bool hasActiveWord;

    private void Start()
    {
        TextComponent.fontStyle = FontStyles.Underline;
        wordToType = word.text.ToLower();
        wordContainer = wordToType;
        typeIndex = 0;
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            typeIndex++;

            word.text = word.text.Remove(0, 1);
            word.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            word.text = wordContainer;
            word.color = Color.green;
        }

        if (typeIndex >= wordToType.Length)
        {
            FindObjectOfType<AudioManager>().Play("Start_Stage_SFX");
            Debug.Log("Open Book");
            SceneManager.LoadScene("Game");
            //levelChanger.FadeToLevel(2);
            levelChanger.FadeToNextLevel();
        }
    }
}
