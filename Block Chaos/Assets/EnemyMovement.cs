using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private PlayerSpawnBlock pSpawnBlock;
    private Vector3 currentDestination = Vector3.zero;
    private Transform currentObstacleTarget;
    private float damage;
    private float obstacleHitRate;
    public enum NavMode {normal, blocked, attacking};
    public enum AIMode {normal, settingBlocked, checkingRerouteBlock, gettingNewRoute, goingNewRoute, searchingPlayerObs, searchingObs, attackingObs}
    

    [Header("NavMeshInfo")]
    //public float checkRate;
    public float areaRange;
    public float maxSearchRange; // max search for rerouting path
    public float remainingDistance; // remaining distance to rerouted point before activate attack mode
    public float turnRate;
    
    
    

    [Header("Attachment")]
    public Transform raycastPos;

    [Header("View Only")]
    public AIMode aiMode;

    public NavMode currentNavMode = NavMode.normal;
    private int playerLayer;
    private int obstacleLayer;
    private int tryGetRouteCount = 0;
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        damage = GetComponent<Enemy>().damage;
        playerLayer = LayerMask.GetMask("Player");
        obstacleLayer = LayerMask.GetMask("Obstacle");
        obstacleHitRate = GetComponent<Enemy>().obstacleHitRate;
        StartCoroutine(CheckForBlockPath());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNavMode == NavMode.normal)
        {
            navMeshAgent.SetDestination(player.position);
        }
        if (DebugMode.AIDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                print("Has path: " + navMeshAgent.hasPath);
                print("Remaining distance: " + navMeshAgent.remainingDistance);
                NavMeshPath path = new NavMeshPath();
                navMeshAgent.CalculatePath(player.position, path);
                print("Path status: " + path.status);
                if (currentDestination != null)
                {
                    print("Current destination: " + currentDestination);
                }
            }
        }
    }
    
    private IEnumerator CheckForBlockPath()
    {
        while (GameManager.gameOn)
        {
            bool getNewPath = false;

            // ----------- [ CHECK PLAYER PATH ] ----------------

            NavMeshPath path = new NavMeshPath();
            navMeshAgent.CalculatePath(player.position, path);



            //Normal -> Blocked | If player path is blocked
            if ((currentNavMode == NavMode.normal && path.status == NavMeshPathStatus.PathPartial) || (!navMeshAgent.hasPath && path.status ==  NavMeshPathStatus.PathComplete))
            {//If path is blocked. Execute block path
                if (DebugMode.AIDebugMode)
                {
                    print("[BLOCKED] OI ITS BLOCKED!");
                }
                aiMode = AIMode.settingBlocked;
                currentNavMode = NavMode.blocked;
                getNewPath = true;
                tryGetRouteCount = 0;
            }
            // Any state other than normal --> Normal | If player path is not blocked
            else if (currentNavMode != NavMode.normal && path.status == NavMeshPathStatus.PathComplete)
            { // If got path to player. Then work as normal
                currentNavMode = NavMode.normal;
                aiMode = AIMode.normal;
                getNewPath = false;
                currentDestination = Vector3.zero;
                if (DebugMode.AIDebugMode)
                {
                    print("[NORMAL] OKAY back to normal");
                }
                tryGetRouteCount = 0;
            }

            // ----------- [ CHECK REROUTED PATH ] ----------------
            //Blocked --> blocked | If rerouted path is blocked
            if (currentDestination != Vector3.zero && currentNavMode == NavMode.blocked)
            {
                NavMeshPath reroutedPath = new NavMeshPath();
                navMeshAgent.CalculatePath(currentDestination, reroutedPath);
                aiMode = AIMode.checkingRerouteBlock;
                if (DebugMode.AIDebugMode)
                {
                    print("Rerouted Path: " + reroutedPath.status);
                }
                // PATH BLOCKED
                if (reroutedPath.status == NavMeshPathStatus.PathPartial)
                {
                    tryGetRouteCount += 1;
                    getNewPath = true;
                    if (DebugMode.AIDebugMode)
                    {
                        print("[REROUTE BLOCKED] Geting another new point.");
                    }
                }
                // REACHED 
                else if (navMeshAgent.remainingDistance <= remainingDistance || !navMeshAgent.hasPath)
                {
                    tryGetRouteCount = 0;
                    currentNavMode = NavMode.attacking;
                    StartCoroutine(attackObstacles());
                }
            }

            // ----------- [ GET NEW PATH ] ----------------
            //Executed when navmode is on blocked or rerouting path
            //Get a new path

            if (getNewPath)
            {
                bool canReach = false;
                int rangeCount = 0;
                float minRange = 0f;
                while (!canReach)
                {
                    if (tryGetRouteCount >= 25)
                    {
                        tryGetRouteCount = 0;
                        currentNavMode = NavMode.attacking;
                        StartCoroutine(attackObstacles());
                    }
                    aiMode = AIMode.gettingNewRoute;
                    rangeCount += 1;
                    //Generate new x and z
                    float posZ = UnityEngine.Random.Range(minRange, areaRange);
                    if (UnityEngine.Random.Range(1,3)==2)
                    {
                        posZ = -posZ;
                    }
                    float posX = UnityEngine.Random.Range(minRange, areaRange);
                    if (UnityEngine.Random.Range(1,3) == 2)
                    {
                        posX = -posX;
                    }
                    //Get new path
                    NavMeshPath newPath = new NavMeshPath();
                    Vector3 newDestination = new Vector3(player.position.x + posX, player.position.y, player.position.z+posZ);
                    navMeshAgent.CalculatePath(newDestination, newPath);
                    
                    if (newPath.status == NavMeshPathStatus.PathComplete || areaRange >= maxSearchRange)
                    {
                        //If more than 25 give up finding new path
                        if (DebugMode.showAIPathFinding)
                        {
                            Debug.DrawRay(newDestination, Vector3.up * 2, Color.red, 5f);
                        }
                        navMeshAgent.SetDestination(newDestination);
                        canReach = true;
                        currentDestination = newDestination;
                        aiMode = AIMode.goingNewRoute;
                        if (DebugMode.AIDebugMode)
                        {
                            print("[REROUTING_BLOCKED] New destination found.");
                        }
                    }
                    else if (rangeCount == 3)
                    {
                        minRange = areaRange;
                        areaRange += 1f;
                        rangeCount = 0;
                    }
                    
                }
            }

            yield return null;
        }

    }


    IEnumerator attackObstacles()
    {
        bool findPlayer = false;
        while (currentNavMode == NavMode.attacking)
        {
            RaycastHit forwardHit;
            //Continue turning character until it looks at player
            while (findPlayer == false && currentNavMode == NavMode.attacking && !Physics.Raycast(raycastPos.position,raycastPos.TransformDirection(Vector3.forward), out forwardHit, Mathf.Infinity, playerLayer))
            {
                aiMode = AIMode.searchingPlayerObs;
                if (DebugMode.AIDebugMode)
                {
                    print("[LOOKING] Look at player");
                }
                //Rotate enemy
                Vector3 playerDir = player.position - raycastPos.position;
                float singleStep = turnRate * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(raycastPos.forward, playerDir, singleStep, 0.0f);
                //newDir = new Vector3(transform.position.x, newDir.y, transform.position.z);
                transform.rotation = Quaternion.LookRotation(newDir);
                yield return null;
            }
            findPlayer = true;
            //Check what obstacle infront of him
            
            if(!Physics.Raycast(raycastPos.position, raycastPos.TransformDirection(Vector3.forward), out forwardHit, 4, obstacleLayer))
            {
                aiMode = AIMode.searchingObs;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x+1f, transform.eulerAngles.y + 1f, transform.eulerAngles.z);
            }
            else if(Physics.Raycast(raycastPos.position, raycastPos.TransformDirection(Vector3.forward), out forwardHit, 4, obstacleLayer))
            {
                aiMode = AIMode.attackingObs;
                if (DebugMode.AIDebugMode)
                {
                    print("[OBSTACLE FOUND] Destroying targetted obstacle.");
                }
                
                currentObstacleTarget = forwardHit.transform;
                
                while (currentObstacleTarget != null && currentNavMode == NavMode.attacking)
                {
                    yield return new WaitForSeconds(obstacleHitRate);
                    if (currentObstacleTarget != null && currentNavMode == NavMode.attacking)
                    {
                        try
                        {
                            currentObstacleTarget.GetComponent<Obstacle>().onEnemyHit(damage);
                        }
                        catch (NullReferenceException err)
                        {
                            if (DebugMode.AIDebugMode)
                            {
                                Debug.Log("[CATCHED ERRROR] Error in selecting already destroyed obj: "+ err);
                            }
                            currentObstacleTarget = null;
                        }
                    }
                }
            }
            else
            {
                if (DebugMode.AIDebugMode)
                {
                    Debug.Log("[AI ERROR] Error. Not supposed to reach here");
                }
            }
            yield return null;
        }
    }
}


