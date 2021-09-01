using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardSetRotation : MonoBehaviour
{
    public Vector3 rotation;

    private void LateUpdate()
    {
        transform.eulerAngles = rotation;
    }
}
