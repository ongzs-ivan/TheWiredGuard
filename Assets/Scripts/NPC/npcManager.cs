using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    public static npcManager instance;

    private int npcLimit = 100;
    private int initialNPCnumber = 50;
    private int criminalNumber;
    static public int npcNumber = 0;
    private List<Transform> spawnLocations;
    private List<Transform> spawnDestinations;

    private WaitForSeconds delay = new WaitForSeconds(1.0f);
    private WaitForSeconds extendedDelay = new WaitForSeconds(15.0f);
    private int randomPoint;
    private Vector3 randomSpawnPoint;

    [Header("Spawn & Destination Points")]
    public LocationManager areas;
    
    public GameObject citizenPrefab;
    public GameObject criminalPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        criminalNumber = LevelManager.instance.currentMission.maxTargetCount;
        InitialSpawnNPC();
        StartCoroutine(RealtimeSpawnNPC());
    }

    public void InitialSpawnNPC()
    {
        for (int i = 0; i < initialNPCnumber; i++)
        {
            randomSpawnPoint = areas.GetRandomPoint();
            GameObject civilian = ObjectPooler.instance.GetPooledObject("Civilian");
            if (civilian != null)
            {
                civilian.transform.position = randomSpawnPoint;
                civilian.transform.rotation = Quaternion.identity;
                civilian.SetActive(true);
            }
            civilian.GetComponent<NPC>().SetDestinationList(areas);
        }

        for (int i = 0; i < criminalNumber; i++)
        {
            randomSpawnPoint = areas.GetRandomPoint();
            GameObject criminal = ObjectPooler.instance.GetPooledObject("Criminal");
            if (criminal != null)
            {
                criminal.transform.position = randomSpawnPoint;
                criminal.transform.rotation = Quaternion.identity;
                criminal.SetActive(true);
            }
            criminal.GetComponent<NPC>().SetDestinationList(areas);
        }
    }

    IEnumerator RealtimeSpawnNPC()
    {
        while (true)
        {
            if (npcNumber > 100)
            {
                yield return extendedDelay;
            }
            else if (npcNumber <= 100)
            {
                // Set Spawn
                randomSpawnPoint = areas.GetRandomPoint();

                //Spawn NPC
                GameObject civilian = ObjectPooler.instance.GetPooledObject("Civilian");
                if (civilian != null)
                {
                    civilian.transform.position = randomSpawnPoint;
                    civilian.transform.rotation = Quaternion.identity;
                    civilian.SetActive(true);
                }

                // Set spawned NPC destination
                civilian.GetComponent<NPC>().SetDestinationList(areas);

                //Debug.Log("Spawning npc");
                yield return delay;
            }
        }
    }
}
