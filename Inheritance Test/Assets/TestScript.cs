using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public TowerAttackBehaviour_Base script;
    [SerializeField]
    private List<string> targetName;
    public string hello;
    private void Awake()
    {
    }
    void StartTest()
    {
        List<string> targetName1 = new List<string>();
        targetName1.Add("Target A");
        targetName1.Add("Target B");
        targetName1.Add("Target C");
        targetName1.Add("Target D");
        targetName1.Add("Target E");
        targetName = targetName1;
        script.UpdateTargetList(targetName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            StartTest();
        if (Input.GetKeyDown(KeyCode.Z))
            AddNewTarget();
        if (Input.GetKeyDown(KeyCode.X))
            RemoveTarget();
        if (Input.GetKeyDown(KeyCode.C))
            RemoveAllTarget();
    }

    private void RemoveAllTarget()
    {
        targetName.Clear();
        script.UpdateTargetList(targetName);
    }

    private void RemoveTarget()
    {
        targetName.Remove(targetName[UnityEngine.Random.Range(0, targetName.Count)]);
        script.UpdateTargetList(targetName);
    }

    //Simulation scenario
    // - 1 dies , 2 dies, all dies
    //Along with new and non new
    // Completely new target
    //Change Order
    private void AddNewTarget()
    {
        string name;
        do
        {
            name = "Target " + UnityEngine.Random.Range(1, 100);
        } while (targetName.Contains(name));
        targetName.Add(name);
        script.UpdateTargetList(targetName);
    }
}
