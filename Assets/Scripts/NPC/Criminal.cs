using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal : NPC
{
    public override void OnDeath()
    {
        LevelManager.instance.UpdateMissionStatus(1, 0);
        gameObject.SetActive(false);
    }
}
