using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{
    public Camera cinematicCamera;
    public float rotateSensitivity = 1f;
    private float rotateAngle = 5f;
    private float flySpeed = 5f;
    public float flySensitivity = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSensitivity, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotateAngle * Time.deltaTime * rotateSensitivity, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(rotateAngle * Time.deltaTime * rotateSensitivity, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-rotateAngle * Time.deltaTime * rotateSensitivity, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (flySpeed * Time.deltaTime * flySensitivity), transform.position.z);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (flySpeed * Time.deltaTime * flySensitivity), transform.position.z);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x + (flySpeed * Time.deltaTime * flySensitivity), transform.position.y , transform.position.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x - (flySpeed * Time.deltaTime * flySensitivity), transform.position.y, transform.position.z);
        }
    }
}
