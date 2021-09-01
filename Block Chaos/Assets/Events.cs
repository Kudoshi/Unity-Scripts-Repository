using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Events : MonoBehaviour
{
    public static event Action<string, float> onInfoTopMsg;

    public static void invokeOnInfoTopMsg(string msg, float duration)
    {
        onInfoTopMsg?.Invoke(msg, duration);
    }
}
