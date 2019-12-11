using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;
    
    private void OnEnable()
    {
        ExplosionDamage(transform.position, explosionRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, explosionRadius);
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask, QueryTriggerInteraction.UseGlobal);
        int i = 0;
        while (i < hitColliders.Length)
        {
            hitColliders[i].gameObject.SetActive(false);
            //Debug.Log("Kill NPC");
            i++;
        }
    }
}
