using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    private int npcLimit = 100;
    private int npcNumber = 0;
    private List<Transform> spawnLocations;
    private List<Transform> spawnDestinations;
    private GameObject npcParent;

    private WaitForSeconds delay = new WaitForSeconds(1.0f);
    private int randomPoint;

    [Header("Spawn & Destination Points")]
    public LocationManager spawnLocs;
    public LocationManager destinationLocs;
    
    public GameObject npcPrefab;

    void Start()
    {
        spawnLocations = spawnLocs.GetLocations();
        spawnDestinations = destinationLocs.GetLocations();
        npcParent = new GameObject("NPC Parent");
        StartCoroutine(SpawnNPC());
    }

    IEnumerator SpawnNPC()
    {
        while (npcLimit <= 100)
        {
            yield return delay;
            Debug.Log("Spawning npc");
            // Set Spawn
            randomPoint = Random.Range(0, spawnLocations.Count);
            GameObject npc = Instantiate(npcPrefab, spawnLocations[randomPoint].transform.position ,Quaternion.identity) as GameObject;
            npc.transform.SetParent(npcParent.transform);
            // Set Destination
            randomPoint = Random.Range(0, spawnDestinations.Count);
            npc.GetComponent<NPC>().SetDestination(spawnDestinations[randomPoint].transform);
        }
    }
}
