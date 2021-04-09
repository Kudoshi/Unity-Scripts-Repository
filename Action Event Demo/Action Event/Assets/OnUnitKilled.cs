using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnUnitKilled : MonoBehaviour
{
    //Message sent nicely good.
    public string message;
    public static Action<string> messageToBeDisplayed;

    private void OnEnable()
    {
        messageToBeDisplayed?.Invoke(message);
    }
}
