using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(RecalculatePathRoutine());
    }
    
    IEnumerator RecalculatePathRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return delay;
            agent.SetDestination(target.position);
        }
    }

    public void SetDestination(Transform newDestination)
    {
        target = newDestination;
    }
}
