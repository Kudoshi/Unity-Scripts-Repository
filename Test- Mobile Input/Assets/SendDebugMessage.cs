using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDebugMessage : MonoBehaviour
{
    public string message;

    public void OnButtonPress()
    {
        Debug.Log(message);
    }
}
