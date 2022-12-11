using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;
    public string practiceModeScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void PracticeMode()
    {
        SceneManager.LoadScene(practiceModeScene);
    }
}
