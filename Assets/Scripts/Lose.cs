using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{

    public GameObject LoseScene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneHistory.Instance.PreviousScene();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}