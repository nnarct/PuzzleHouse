using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    public bool isPaused;
    private Interactor interactorScript;
    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        pauseMenu.SetActive(false);
        interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playerSpriteRenderer.color = new Color(0.3608f, 0.3608f, 0.3608f);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        playerSpriteRenderer.color = new Color(1f, 1f, 1f);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}

