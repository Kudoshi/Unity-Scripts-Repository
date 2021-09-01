using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerSpawnBlock : MonoBehaviour
{
    public GameObject ObstaclePf;
    public float spawnDist;
    public NavMeshAgent agent;
    public float checkBlockDuration;
    public BlockChecker blockChecker;

    private Player player;
    private float endCheckBlockTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("PlaceObj") && player.currentBlock > 0)
        {
            blockChecker.checkBlock(true);
        }
        if (Input.GetButtonUp("PlaceObj") && player.currentBlock > 0)
        {
            blockChecker.checkBlock(false);
            bool canBuild = blockChecker.checkCanBuild();
            if (canBuild)
            {
                player.UpdateStats("block", -1);
                Vector3 spawnLocation = transform.position + (transform.forward * spawnDist);
                spawnLocation = new Vector3(spawnLocation.x, spawnLocation.y + 0.2f, spawnLocation.z);
                GameObject block = Instantiate(ObstaclePf, spawnLocation, ObstaclePf.transform.rotation);

            }
        }
    }
}
