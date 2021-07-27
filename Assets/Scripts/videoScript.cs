using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoScript : MonoBehaviour
{

    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public GameObject transition;
    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(LoadLevel1());
    }
    IEnumerator LoadLevel1()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "cutscene1")
        {
            transition.SetActive(true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Level1");
        }
        if (currentScene == "cutscene2")
        {
            transition.SetActive(true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("MainMenu");
        }

    }
}
