using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackBehaviour : TowerAttackBehaviour_Base
{
    [Header("Tweak settings")]
    public float displayInterval;
    [Header("View Only")]
    public List<string> targetList;
    public string currentTarget { get; set; }
    public bool shouldAttackEnemy;
    private void Start()
    {
        StartCoroutine(attackEnemy());
    }

    public bool loopAttack = true;
    public IEnumerator attackEnemy()
    {
        //Constantly loops over and over. If there are no enemies. ShouldAttackEnemy would be false.
        while (loopAttack)
        {
            yield return new WaitForSeconds(0.05f);
            if (shouldAttackEnemy)
            {
                yield return new WaitForSeconds(displayInterval);
                Debug.Log("Attacking : " + currentTarget);
            }
        }
    }
    public override void UpdateTargetList(List<string> targetList)
    {
        this.targetList = targetList;

        //Functions below does not execute if target is still in range (or not dead)
        if (targetList.Count == 0)
        {
            StopAttacking();
        }
        else if (!targetList.Contains(currentTarget)) //Executed when currentTarget(null or dead target) not in the list.
        {
            GetNewTarget();
        }

    }
    public void StopAttacking()
    {
        shouldAttackEnemy = false;
        currentTarget = "Null";
    }
    public void GetNewTarget()
    {
        shouldAttackEnemy = true;
        currentTarget = targetList[Random.Range(0, targetList.Count)]; //random.range int excludes the max so no need to -1
    }
}
