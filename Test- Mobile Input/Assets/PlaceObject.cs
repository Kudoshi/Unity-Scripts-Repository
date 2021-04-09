using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Transform objectParent;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BuildBuilding()
    {
        GameObject building = Instantiate(buildingPrefab, gameObject.transform.position, gameObject.transform.rotation, objectParent);
        building.transform.localScale = gameObject.transform.localScale;
    }
}
