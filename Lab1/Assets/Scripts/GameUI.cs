using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject nextPlayerButton;
    public GameObject endscreen;
    public GameObject wintext;
    public GameObject loseText;
    public GameObject nextLevelButton;
    public GameObject seeMapButton;
    
    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnNextPlayerButton()
    {
        GameManager.instance.SpawnNewPlayer();
        nextPlayerButton.SetActive(false);
    }

    public void OnNextLevel()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex;
        sceneNo += 2;
        SceneManager.LoadScene("Level " + sceneNo);
    }

    public void OnLastLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnViewMap()
    {
        CameraController.instance.SeeMap();
    }


    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetEndScreen(bool didWin)
    {
        endscreen.SetActive(true);
        wintext.SetActive(didWin);
        nextLevelButton.SetActive(didWin);
        loseText.SetActive(!didWin);
        seeMapButton.SetActive(false);
    }
}
