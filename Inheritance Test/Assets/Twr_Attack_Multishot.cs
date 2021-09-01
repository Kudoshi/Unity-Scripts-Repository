using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Twr_Attack_Multishot : TowerAttackBehaviour_Base
{
    [Header("Tweak")]
    public int numberOfAttacks;
    public float atkInterval;
    [Header("View-only")]
    public List<string> currentTargetList;
    public List<string> targettingList;
    public Coroutine[] attackOrder;
    private void Awake()
    {
        targettingList = new List<string>();
        for (int i = 0; i < numberOfAttacks; i++)
        {
            targettingList.Add(null);
        }
        attackOrder = new Coroutine[numberOfAttacks];
        Debug.Log("Number of attackOrder : " + attackOrder.Count());
    }
    public override void UpdateTargetList(List<string> targetList)
    {
        this.currentTargetList = targetList.ToList();

        if (currentTargetList.Count == 0)
        {
            StopAttacking();
            return;
        }
        //foreach current target list. Check if its in the given target list. If not remove
        Debug.Log("------------------------------------------------------");
        for (int i = 0; i < numberOfAttacks; i++) //foreach target in targetting list
        {
            if (currentTargetList.Count == 0)
            {
                Debug.Log("No more target to lock on");
                for (int x = i; x < numberOfAttacks; x++)
                {
                    targettingList[x] = null;
                }
                return;
            }

            if (currentTargetList.Contains(targettingList[i]))
            {
                //Contains
                string target = currentTargetList.First(item => item == targettingList[i]);
                currentTargetList.Remove(target);
            }
            else if (!currentTargetList.Contains(targettingList[i]))
            {
                if (attackOrder[i] != null)
                    StopCoroutine(attackOrder[i]);
                string target = currentTargetList[UnityEngine.Random.Range(0, currentTargetList.Count)];
                targettingList[i] = target;
                currentTargetList.Remove(target);
                Coroutine atkOrder = StartCoroutine(Attack(targettingList[i]));
                attackOrder[i] = atkOrder;
                //Find new target


            }
        }

    }
    public IEnumerator Attack(string target)
    {
        while (target != null)
        {
            Debug.Log("Attack Enemy: " + target);
            yield return new WaitForSeconds(atkInterval);
            yield return null;
        }
        Debug.Log("Enemy dead : " + target);
    }
    private void StopAttacking()
    {
        for (int i = 0; i < numberOfAttacks; i++)
        {
            targettingList[i] = null;
        }
        StopAllCoroutines();
    }
}
