using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnNewGameButtonClick);
    }

    private void OnNewGameButtonClick()
    {
        SceneManager.LoadScene("OptimizedMap");
    }

    public void StartUpLevel()
    {
        
    }
}
