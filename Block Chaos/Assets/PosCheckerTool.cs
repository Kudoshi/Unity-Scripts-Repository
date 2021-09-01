using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PosCheckerTool : MonoBehaviour
{
    public Vector3 currentPos;

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
    }
}
