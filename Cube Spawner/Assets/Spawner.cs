using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objPf;
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            InvokeRepeating("SpawnCubes", 0f, 0.2f);

        }

    }

    public void SpawnCubes()
    {
        Instantiate(objPf, transform.position, objPf.transform.rotation, transform);
    }
}
