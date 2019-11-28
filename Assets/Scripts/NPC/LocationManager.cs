using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private List<Transform> locations;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            locations.Add(transform.GetChild(i).GetComponent<Transform>());
        }
    }

    public List<Transform> GetLocations()
    {
        return locations;
    }
}
