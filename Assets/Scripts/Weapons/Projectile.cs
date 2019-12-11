using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform defaultTransform;
    
    private void Start()
    {
        defaultTransform = this.transform;
    }

    private void ResetToDefault()
    {
        gameObject.transform.position = defaultTransform.position;
        gameObject.transform.rotation = defaultTransform.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnEnable()
    {
        GameObject firingVFX = ObjectPooler.instance.GetPooledObject("Firing VFX");
        if (firingVFX != null)
        {
            firingVFX.transform.position = transform.position;
            firingVFX.transform.rotation = transform.rotation;
            firingVFX.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No speed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit");
        ResetToDefault();

        // spawns explosion
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject explosion = ObjectPooler.instance.GetPooledObject("Explosion");
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.rotation = transform.rotation;
            explosion.SetActive(true);
        }

        // bullet is destroyed
        gameObject.SetActive(false);
    }
    
}
