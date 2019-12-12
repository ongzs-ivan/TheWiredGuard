using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    public static npcManager instance;

    private int npcLimit = 100;
    private int initialNPCnumber = 50;
    static public int npcNumber = 0;
    private List<Transform> spawnLocations;
    private List<Transform> spawnDestinations;

    private WaitForSeconds delay = new WaitForSeconds(1.0f);
    private WaitForSeconds extendedDelay = new WaitForSeconds(15.0f);
    private int randomPoint;
    private Vector3 randomSpawnPoint;

    [Header("Spawn & Destination Points")]
    public LocationManager areas;
    
    public GameObject npcPrefab;

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

    void Start()
    {
        InitialSpawnNPC();
        StartCoroutine(RealtimeSpawnNPC());
    }

    private void InitialSpawnNPC()
    {
        for (int i = 0; i < initialNPCnumber; i++)
        {
            randomSpawnPoint = areas.GetRandomPoint();
            GameObject npc = ObjectPooler.instance.GetPooledObject("NPC");
            if (npc != null)
            {
                npc.transform.position = randomSpawnPoint;
                npc.transform.rotation = Quaternion.identity;
                npc.SetActive(true);
            }
            npc.GetComponent<NPC>().SetDestinationList(areas);
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
                GameObject npc = ObjectPooler.instance.GetPooledObject("NPC");
                if (npc != null)
                {
                    npc.transform.position = randomSpawnPoint;
                    npc.transform.rotation = Quaternion.identity;
                    npc.SetActive(true);
                }

                // Set spawned NPC destination
                npc.GetComponent<NPC>().SetDestinationList(areas);

                //Debug.Log("Spawning npc");
                yield return delay;
            }
        }
    }


}
