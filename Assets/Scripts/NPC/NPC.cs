using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    private NavMeshAgent agent;
    private LocationManager destinationArea;
    private WaitForSeconds delay = new WaitForSeconds(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destinationArea.GetRandomPoint());

        StartCoroutine(RecalculatePathRoutine());
    }
    
    IEnumerator RecalculatePathRoutine()
    {
        while (true)
        {
            yield return delay;

            if (agent.remainingDistance < 5.0f)
            {
                agent.SetDestination(destinationArea.GetRandomPoint());
            }
        }
    }

    private GameObject GetDestination()
    {
        GameObject go = ObjectPooler.instance.GetPooledObject("Target Destination");
        if (go != null)
        {
            go.transform.position = destinationArea.GetRandomPoint();
            go.transform.rotation = Quaternion.identity;
            go.SetActive(true);
        }
        return go;
    }

    public void SetDestinationList(LocationManager newDestinationAreas)
    {
        destinationArea = newDestinationAreas;
    }

    public abstract void OnDeath();
}
