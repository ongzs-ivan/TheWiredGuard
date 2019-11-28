using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    public Vector3 MoveVector { set; get; }

    [Header("Input Dependencies")]
    [SerializeField] VirtualStickInput leftStick;

    private void Update()
    {
        MoveVector = PoolInput();
        Move();
        RotatePlayer();
    }

    private void Move()
    {
        this.transform.Translate(MoveVector * Time.deltaTime * moveSpeed, Space.World);
    }

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = leftStick.Horizontal();
        dir.z = leftStick.Vertical();

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }

    private void RotatePlayer()
    {
        Vector3 dir = new Vector3(leftStick.Horizontal(),0.0f,leftStick.Vertical());
        //Vector3 dir = Vector3.zero;

        if (dir != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(a: transform.rotation, b: Quaternion.LookRotation(dir), t: rotateSpeed * Time.deltaTime);
        }
    }
}
