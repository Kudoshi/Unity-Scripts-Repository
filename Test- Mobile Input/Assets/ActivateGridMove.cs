using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGridMove : MonoBehaviour
{
    public MoveObjectMobileTouch normalMoveScript;
    private bool isPressed;
    public void OnButtonPressed()
    {
        normalMoveScript.SwitchMove();
    }
}
