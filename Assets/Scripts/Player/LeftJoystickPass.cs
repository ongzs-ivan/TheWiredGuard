using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftJoystickPass : MonoBehaviour
{
    public static LeftJoystickPass instance;
    private JoystickInput leftStick;

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

    private void Start()
    {
        leftStick = GetComponent<JoystickInput>();
    }

    public JoystickInput LeftJoystickLocator()
    {
        return leftStick;
    }
}
