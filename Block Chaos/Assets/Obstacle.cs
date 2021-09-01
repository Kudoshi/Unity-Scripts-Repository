using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Obstacle : MonoBehaviour
{
    public float health;
    public GameObject hitGfxPf;
    public Material halfDeadMat;
    private NavMeshSurface navSurface;
    // Start is called before the first frame update
    void Start()
    {
        navSurface = FindObjectOfType<NavMeshSurface>();
        navSurface.BuildNavMesh();
    }

    public void onEnemyHit(float damage)
    {
        print("[OBSTACLE HIT]Got Hit!");
        health -= damage;
        Instantiate(hitGfxPf, transform.position, hitGfxPf.transform.rotation);
        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
        else if (health <= 50)
        {
            GetComponent<MeshRenderer>().material = halfDeadMat;
        }
    }
    private void DestroySelf()
    {

    }
}
