using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private List<SpawnArea> areas;
    
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            areas.Add(transform.GetChild(i).GetComponent<SpawnArea>());
        }
    }

    public Vector3 GetRandomPoint()
    {
        return areas[Random.Range(0, areas.Count)].ReturnRandomSpawnPoint();
    }

    public int ReturnRandomAreaCount()
    {
        return areas.Count;
    }
}
