using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarLockRotation : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        transform.eulerAngles = new Vector3(57, 0, 0);

    }
}
