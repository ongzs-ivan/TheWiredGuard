using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public MissionInfo currentMission;

    public static LevelManager instance;

    public CanvasGroup WarningScreen;
    public CanvasGroup FailScreen;
    public CanvasGroup WinScreen;

    private WaitForSeconds warningDelay = new WaitForSeconds(2.0f);
    private WaitForSeconds delay = new WaitForSeconds(5.0f);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateMissionStatus(int targetKills, int casualtyKills)
    {
        if (targetKills >= 1)
        {
            currentMission.targetList++;
        }

        if (casualtyKills >= 1 && currentMission.casualtyList <= 2)
        {
            currentMission.casualtyList++;
            StartCoroutine(TriggerWarningScreen());
        }

        if (currentMission.targetList >= currentMission.maxTargetCount && currentMission.casualtyList < currentMission.maxCasualtyCount)
        {
            StartCoroutine(TriggerWinMissionScreen());
        }

        else if (currentMission.casualtyList >= currentMission.maxCasualtyCount)
        {
            StartCoroutine(TriggerFailMissionScreen());
        }
    }

    public IEnumerator TriggerWarningScreen()
    {
        yield return new WaitForSeconds(0.5f);
        WarningScreen.gameObject.SetActive(true);
        yield return warningDelay;
        WarningScreen.gameObject.SetActive(false);
    }

    public IEnumerator TriggerFailMissionScreen()
    {
        FailScreen.gameObject.SetActive(true);
        for (float f = 0f; f < 1; f += Time.deltaTime)
        {
            FailScreen.alpha = f;
            yield return null;
        }
        FailScreen.alpha = 1;
        ObjectPooler.instance.DespawnAll();
        yield return delay;
        FailScreen.alpha = 0;
        SceneManager.LoadScene("MainMenu");

    }

    public IEnumerator TriggerWinMissionScreen()
    {
        WinScreen.gameObject.SetActive(true);
        for (float f = 0f; f < 1; f += Time.deltaTime)
        {
            WinScreen.alpha = f;
            yield return null;
        }
        WinScreen.alpha = 1;
        ObjectPooler.instance.DespawnAll();
        yield return delay;
        WinScreen.alpha = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseAllScreen()
    {
        WarningScreen.gameObject.SetActive(false);
        FailScreen.gameObject.SetActive(false);
        WinScreen.gameObject.SetActive(false);
    }
}
