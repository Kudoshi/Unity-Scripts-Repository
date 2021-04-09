using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUnitKilledListener : MonoBehaviour
{
    private void OnEnable()
    {
        OnUnitKilled.messageToBeDisplayed += displayMessage;
    }

    private void displayMessage(string message)
    {
        Debug.Log("Message received : " + message);
    }
}
