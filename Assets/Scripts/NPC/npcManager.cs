using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    public static npcManager instance;

    private int npcLimit = 100;
    private int npcNumber = 0;
    private List<Transform> spawnLocations;
    private List<Transform> spawnDestinations;
    private int spawnLocNumber;
    private int spawnDestinationNumber;
    private GameObject npcParent;

    private WaitForSeconds delay = new WaitForSeconds(1.0f);
    private int randomPoint;

    [Header("Spawn & Destination Points")]
    public LocationManager spawnLocs;
    public LocationManager destinationLocs;
    
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
        spawnLocations = spawnLocs.GetLocations();
        spawnDestinations = destinationLocs.GetLocations();
        spawnLocNumber = spawnLocations.Count;
        spawnDestinationNumber = spawnDestinations.Count;
        StartCoroutine(SpawnNPC());
    }

    IEnumerator SpawnNPC()
    {
        while (npcLimit <= 100)
        {
            yield return delay;
            //Debug.Log("Spawning npc");

            // Set Spawn
            randomPoint = Random.Range(0, spawnLocNumber);
            
            // Set Destination
            randomPoint = Random.Range(0, spawnDestinationNumber);

            GameObject npc = ObjectPooler.instance.GetPooledObject("NPC");
            if (npc != null)
            {
                npc.transform.position = spawnLocations[randomPoint].transform.position;
                npc.transform.rotation = spawnLocations[randomPoint].transform.rotation;
                npc.SetActive(true);
            }

            npc.GetComponent<NPC>().SetDestination(spawnDestinations[randomPoint].transform);

            //npc.transform.SetParent(npcParent.transform);
        }
    }
}
