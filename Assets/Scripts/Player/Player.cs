using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip[] gibClips;
    public Coroutine gibberishCoroutine;

    string currentScene;
    bool isLose = false;
    public bool speakingGib = false;//added this

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (currentScene == "LoseScene" && !isLose)
        {
            Debug.Log("Lose Scene");
            animator.SetBool("isLose", true);
            isLose = true;
        }
    }

    public void StartSpeakingGibberish()
    {
        gibberishCoroutine = StartCoroutine(SpeakingGibberish());
        speakingGib = true; //added this
        Debug.Log("Speaking Gibberish: True");
    }

    public void StopSpeakingGibberish()
    {
        StopCoroutine(gibberishCoroutine);
        speakingGib = false; //added this
        Debug.Log("Speaking Gibberish: False");
    }

    IEnumerator SpeakingGibberish()
    {   
        audioSource.clip = gibClips[Random.Range(0, gibClips.Length)];
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
        gibberishCoroutine = StartCoroutine(SpeakingGibberish());
    }
}
