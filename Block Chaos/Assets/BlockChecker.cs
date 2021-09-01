using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChecker : MonoBehaviour
{
    private float spawnDist;
    public Transform player;
    public GameObject blockCheckerObj;
    public Material noBuildMat;

    private Material originalMat;
    private MeshRenderer blockMesh;
    private void Start()
    {
        if (blockCheckerObj.activeSelf)
        {
            blockCheckerObj.SetActive(false);
        }

        spawnDist = player.GetComponent<PlayerSpawnBlock>().spawnDist;
        blockMesh = blockCheckerObj.GetComponent<MeshRenderer>();
        originalMat = blockMesh.material;
        
        Vector3 spawnLocation = transform.position + (transform.forward * spawnDist);
        spawnLocation = new Vector3(spawnLocation.x, spawnLocation.y + 0.2f, spawnLocation.z);
        blockCheckerObj.transform.position = spawnLocation;

    }
    public void checkBlock(bool spawnCheckBlock)
    {
        if (spawnCheckBlock == true)
        {
            blockCheckerObj.SetActive(true);
        }
        else if (spawnCheckBlock == false)
        {
            blockCheckerObj.SetActive(false);
        }
    }

    public bool checkCanBuild()
    {
        Collider[] hitColliders = Physics.OverlapSphere(blockCheckerObj.transform.position, 0.8f, (1 << LayerMask.NameToLayer("Obstacle")) | (1 << LayerMask.NameToLayer("Enemy")));

        //(1 << LayerMask.NameToLayer("Sight") | (1 << LayerMask.NameToLayer("OtherLayerMaskName"))))
        if (hitColliders.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    

    private void Update()
    {
        if (blockCheckerObj.activeSelf)
        {
            blockCheckerObj.transform.eulerAngles = new Vector3(0, 0, 0);


            Collider[] hitColliders = Physics.OverlapSphere(blockCheckerObj.transform.position, 0.8f, (1 << LayerMask.NameToLayer("Obstacle")) | (1 << LayerMask.NameToLayer("Enemy")));
            if (hitColliders.Length > 0)
            {
                blockMesh.material = noBuildMat;
            }
            else if (blockMesh.material != originalMat)
            {
                blockMesh.material = originalMat;
            }
        }
           
    }
}
