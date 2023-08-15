using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Device;

public class InputManager : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerController.onTouched += TouchControl;
    }
    private void OnDisable()
    {
        PlayerController.onTouched -= TouchControl;
    }

    private float TouchControl(int i)
    {
       return Input.GetTouch(i).position.x;
    }
}
