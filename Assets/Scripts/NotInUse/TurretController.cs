using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Tooltip("Should turret rotate in the FixedUpdate rather than Update?")]
    public bool runRotationsInFixed = false;

    [Header("Objects")]
    [Tooltip("Transform used to provide the horizontal rotation of the turret.")]
    public Transform turretBase;
    [Tooltip("Transform used to provide the vertical rotation of the barrels. Must be a child of the TurretBase.")]
    public Transform turretBarrels;

    [Header("Rotation Limits")]
    [Tooltip("Turn rate of the turret's base and barrels in degrees per second.")]
    public float turnRate = 30.0f;
    [Tooltip("When true, turret rotates according to left/right traverse limits. When false, turret can rotate freely.")]
    public bool limitTraverse = false;
    [Tooltip("When traverse is limited, how many degrees to the left the turret can turn.")]
    [Range(0.0f, 180.0f)]
    public float leftTraverse = 60.0f;
    [Tooltip("When traverse is limited, how many degrees to the right the turret can turn.")]
    [Range(0.0f, 180.0f)]
    public float rightTraverse = 60.0f;
    [Tooltip("How far up the barrel(s) can rotate.")]
    [Range(0.0f, 90.0f)]
    public float elevation = 60.0f;
    [Tooltip("How far down the barrel(s) can rotate.")]
    [Range(0.0f, 90.0f)]
    public float depression = 5.0f;

    //[Header("Movement & Rotation")]
    //[SerializeField] float rotSpeed = 90f;
    //[SerializeField] float xRotClampRange = 30f;
    //private float xRot = 0f;

    //[Header("Transform Refs")]
    //[SerializeField] Transform camRoot;

    [Header("Utilities")]
    [Tooltip("Show the arcs that the turret can aim through.\n\nRed: Left/Right Traverse\nGreen: Elevation\nBlue: Depression")]
    public bool showArcs = false;
    [Tooltip("When game is running in editor, draws a debug ray to show where the turret is aiming.")]
    public bool showDebugRay = true;

    [Header("Input Dependencies")]
    [SerializeField] VirtualStickInput leftStick;

    private Vector3 aimPoint;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
