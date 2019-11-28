using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LineRenderer liner;

    private void Start()
    {
        StartCoroutine( RecalculatePathRoutine() );

        liner.widthMultiplier = 0.2f;
    }

    /// <summary>
    /// Periodically recalculate the destination to the target
    /// </summary>
    IEnumerator RecalculatePathRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return delay;
            agent.SetDestination(target.position);
        }
    }

    private void LateUpdate()
    {
        if (agent.hasPath)
        {
            for(int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Vector3 start = agent.path.corners[i];
                Vector3 end = agent.path.corners[i + 1];

                Debug.DrawLine(start, end, Color.red);
            }
        }


        liner.positionCount = agent.path.corners.Length;

        Vector3[] offsetCorners = agent.path.corners;

        for (int i = 0; i <offsetCorners.Length; i++)
        {
            offsetCorners[i] += Vector3.up;
        }

        liner.SetPositions(offsetCorners);
    }
}
