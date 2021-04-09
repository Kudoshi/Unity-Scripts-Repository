using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnAwake : MonoBehaviour
{
    public UnityEvent events;
    void Start()
    {
        events?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
