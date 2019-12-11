﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootWeapon : MonoBehaviour, IPointerClickHandler
{
    public static ShootWeapon instance;

    [SerializeField] Transform firingBarrel;
    [SerializeField] float speed = 100f;

    [SerializeField] GameObject bulletPrefab;

    private Quaternion bulletRotation;
    private Vector3 bulletDirection;
    private GameObject bulletParent;

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

    public void SetFiringSpeed(float newFiringSpeed)
    {
        speed = newFiringSpeed;
    }

    public void OnPointerClick(PointerEventData eventData) //fires on press and release
    {
        bulletRotation = firingBarrel.transform.rotation;
        bulletDirection = firingBarrel.forward;

        //GameObject bullet = Instantiate(bulletPrefab, firingBarrel.position, bulletRotation) as GameObject;
        
        GameObject bullet = ObjectPooler.instance.GetPooledObject("Player Bullet");
        Rigidbody instBulletRigidBody = bullet.GetComponent<Rigidbody>();
        if (bullet != null)
        {
            bullet.transform.position = firingBarrel.position;
            bullet.transform.rotation = bulletRotation;
            bullet.SetActive(true);

            instBulletRigidBody.AddForce(bulletDirection * speed);
        }
    }
    
    public void SetFiringBarrel(Transform newBarrelPos)
    {
        firingBarrel = newBarrelPos;
    }
}
