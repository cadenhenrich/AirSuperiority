using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameUI;
    private GameObject deathUI;

    public void RestartGame()
    {
	SceneManager.LoadScene("Main");
    }

    public void ToMainMenu()
    {
	SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
	Application.Quit();
    }

    void Start()
    {
	gameUI = GameObject.FindGameObjectWithTag("GameUI");
	deathUI = GameObject.Find("DeathUI");

	deathUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
	if (!GameObject.FindGameObjectWithTag("Player"))
	{
	    deathUI.SetActive(true);
	    gameUI.SetActive(false);
	}
    }
}
