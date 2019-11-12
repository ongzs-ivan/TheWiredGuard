using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement & Rotation")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotSpeed = 90f;
    [SerializeField] float xRotClampRange = 30f;
    private float xRot = 0f;

    [Header("Transform Refs")]
    [SerializeField] Transform camRoot;

    [Header("Input Dependencies")]
    [SerializeField] VirtualStickInput leftStick;
    [SerializeField] VirtualStickInput rightStick;

    //private void Update()
    //{
    //    MovePlayer();
    //    RotatePlayer();
    //}

    //private void RotatePlayer()
    //{
    //    // Rotate on Y axis
    //    transform.Rotate(0, rightStick.Direction.x * rightStick.Magnitude.x * Time.deltaTime * rotSpeed, 0f);

    //    // Rotate on X axis
    //    xRot += rightStick.Direction.y * rightStick.Magnitude.y * Time.deltaTime * rotSpeed;
    //    xRot = Mathf.Clamp(xRot, -xRotClampRange, xRotClampRange);

    //    camRoot.rotation = Quaternion.Euler(-xRot, transform.rotation.eulerAngles.y, 0);
        
    //    //camRoot.Rotate(rightStick.Direction.y * Time.deltaTime * rotSpeed, 0f, 0f);
    //}

    //private void MovePlayer()
    //{
    //    Vector3 velocity = new Vector3(leftStick.Direction.x, 0, leftStick.Direction.y);
    //    velocity.x *= leftStick.Magnitude.x;
    //    velocity.y *= leftStick.Magnitude.y;


    //    transform.Translate(velocity * Time.deltaTime * moveSpeed);
    //}

}
