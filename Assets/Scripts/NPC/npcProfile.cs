using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class npcProfile
{
    private string firstName;
    private string surName;
    private npcType occupation;
    private int crimeRecord;

    public npcProfile(string n1, string n2, npcType type, int no)
    {
        firstName = n1;
        surName = n2;
        occupation = type;
        crimeRecord = no;
    }
}

public enum npcType
{
    Civilian,
    Warden,
    Police,
    Criminal,
    Raider
}