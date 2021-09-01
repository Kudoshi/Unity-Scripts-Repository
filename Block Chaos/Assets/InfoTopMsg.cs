using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class InfoTopMsg : MonoBehaviour
{
    public float msgDissapearDur;
    public TextMeshProUGUI text;

    private string currentMsg;
    private void Awake()
    {
        text.text = "";
    }
    private void OnEnable()
    {
        Events.onInfoTopMsg += infoTopMsg;
    }

  

    private void OnDisable()
    {
        Events.onInfoTopMsg -= infoTopMsg;
    }
    private void infoTopMsg(string msg, float duration)
    {
        text.text = msg;
        currentMsg = msg;
        StartCoroutine(resetTopMsg(msg, duration));
    }

    private IEnumerator resetTopMsg(string msg, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (msg == currentMsg)
        {
            text.text = "";
        }
        
    }

    
}
