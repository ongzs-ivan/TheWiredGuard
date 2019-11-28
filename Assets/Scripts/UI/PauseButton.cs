using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerClickHandler
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    
    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PauseGame();
    }
}
