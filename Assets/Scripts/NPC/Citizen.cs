using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : NPC
{
    public override void OnDeath()
    {
        LevelManager.instance.UpdateMissionStatus(0, 1);
        gameObject.SetActive(false);
    }
}
