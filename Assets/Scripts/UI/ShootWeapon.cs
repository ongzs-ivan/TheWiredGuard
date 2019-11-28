using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootWeapon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform firingBarrel;
    [SerializeField] float speed = 100f;

    [SerializeField] GameObject bulletPrefab;

    private Quaternion bulletRotation;
    private Vector3 bulletDirection;
    private GameObject bulletParent;

    private void Start()
    {
        bulletParent = new GameObject("Bullet Parent");
    }

    public void SetFiringSpeed(float newFiringSpeed)
    {
        speed = newFiringSpeed;
    }

    public void OnPointerClick(PointerEventData eventData) //fires on press and release
    {
        bulletRotation = firingBarrel.transform.rotation;
        bulletDirection = firingBarrel.forward;

        GameObject bullet = Instantiate(bulletPrefab, firingBarrel.position, bulletRotation) as GameObject;
        bullet.transform.SetParent(bulletParent.transform);
        Rigidbody instBulletRigidBody = bullet.GetComponent<Rigidbody>();
        instBulletRigidBody.AddForce(bulletDirection * speed);
    }

    //public void OnPointerDown(PointerEventData eventData) // fires on press
    //{
    //    Debug.Log("Firing 2.");
    //}
}
