using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnQuitGameButtonClick);
    }

    private void OnQuitGameButtonClick()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }
}
