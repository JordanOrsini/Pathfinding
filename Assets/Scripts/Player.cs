using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public Transform target;
    Vector3[] path;
    int targetIndex;
    Vector3 currentWaypoint;
    public GameObject a;
    Pathfinding pathFinder;
    Grid myGrid;
    bool done;
    bool selection;
    float speed;

    Animator myAnimator;
    bool walking;

    public AudioSource gameWinSound;
    AudioSource myGameWinSound;

    public AudioSource selectionNoise;
    AudioSource mySelectionNoise;

    public GameObject successText;

    public GameObject myPOVs;

    public Transform myPOVTransforms;

    public GameObject POVController;

    void Start()
    {
        mySelectionNoise = selectionNoise.GetComponent<AudioSource>();
        selection = false;
        myGameWinSound = gameWinSound.GetComponent<AudioSource>();
        done = false;
        pathFinder = a.GetComponent<Pathfinding>();
        myGrid = a.GetComponent<Grid>();
        speed = 0.5f;
        walking = false;
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        myAnimator.SetBool("isWalking", walking);
        if(selection == false)
        {
            //GRID DIJKSTRA
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Instantiate(mySelectionNoise);
                myGrid.turnOnGrid();
                pathFinder.FindPathDijkstra(transform.position, target.position);
                selection = true;
            }
            //GRID A*
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(mySelectionNoise);
                myGrid.turnOnGrid();
                pathFinder.FindPathAStar(transform.position, target.position);
                selection = true;
            }
            //GRID CLUSTER
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                Instantiate(mySelectionNoise);
                myGrid.turnOnGrid();
                myGrid.turnOnClusters();

                selection = true;
            }
            //POV DIJKSTRA
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                Instantiate(mySelectionNoise);
                myPOVs.SetActive(true);
                POVController.GetComponent<POV>().turnOnConnections();
                pathFinder.FindPathDijkstraPOV();
                selection = true;
            }
            //POV A*
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                Instantiate(mySelectionNoise);
                myPOVs.SetActive(true);
                POVController.GetComponent<POV>().turnOnConnections();
                pathFinder.FindPathAStarPOV();
                selection = true;
            }
            //POV CLUSTER
            if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                Instantiate(mySelectionNoise);
                myPOVs.SetActive(true);
                POVController.GetComponent<POV>().turnOnConnections();

                foreach(Transform child in myPOVTransforms)
                {
                    child.gameObject.GetComponent<POVNode>().UsingClusters();
                }

                selection = true;
            }
        }

        if(path != null && done == false && selection == true)
        {
            enableColliders();
           
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    done = true;
                }
                if(done == false)
                {
                    currentWaypoint = path[targetIndex];
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            walking = true;

            Vector3 targetDir = currentWaypoint - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime,0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else
        {
            walking = false;
        }
    }
        
    public void GetPath(Vector3[] waypoints)
    {
        path = waypoints;
        currentWaypoint = path[0];
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(path[i], new Vector3(0.1f, 0.1f, 0.1f));

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.tag == "Target")
        {
            Instantiate(myGameWinSound);
            successText.SetActive(true);

            Destroy(obj.gameObject);
            Destroy(gameObject);
        }
    }

    void enableColliders()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        target.gameObject.GetComponent<Collider>().enabled = true;
    }
}
