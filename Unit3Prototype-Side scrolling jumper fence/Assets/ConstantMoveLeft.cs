using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    private SideScrollMovement sideScrollMovement;
    void Start()
    {
        sideScrollMovement = GameObject.Find("Player").GetComponent<SideScrollMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sideScrollMovement.gameOver == false)
        {
            transform.Translate(Vector3.left * 12 * Time.deltaTime);

        }
    }
}
