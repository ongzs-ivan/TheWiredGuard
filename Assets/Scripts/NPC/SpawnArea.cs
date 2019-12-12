using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;

    private float xPos;
    private float yPos;
    private float zPos;
    private Vector3 randomPos;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    public Vector3 ReturnRandomSpawnPoint()
    {
        xPos = Random.Range((center.x - size.x / 2), (center.x + size.x / 2));
        yPos = Random.Range((center.y - size.y / 2), (center.y + size.y / 2));
        zPos = Random.Range((center.z - size.z / 2), (center.z + size.z / 2));
        randomPos = new Vector3(xPos, yPos, zPos);
        return randomPos;
    }
}
