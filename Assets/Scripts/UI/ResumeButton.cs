using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public GameObject pauseMenuUI; 

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ResumeGame);
    }

    private void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        PauseButton.GameIsPaused = false;

    }
}
