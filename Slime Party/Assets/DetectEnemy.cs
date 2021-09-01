using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public Slime slimeScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            slimeScript.UpdateCreep(other.transform, "add");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            slimeScript.UpdateCreep(other.transform, "remove");
        }
    }
}
