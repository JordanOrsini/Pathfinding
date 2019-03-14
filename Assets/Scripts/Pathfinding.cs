using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour 
{
    public GameObject player;

    Player myPlayer;

    Grid grid;

    List<Vector3> visited = new List<Vector3>(); 
    List<GameObject> visited2 = new List<GameObject>();

    public Transform POVNodes;

    void Awake()
    {
        myPlayer = player.GetComponent<Player>();
        grid = GetComponent<Grid>();
    }
        
    public void FindPathAStar(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos); 

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        visited.Add(startNode.worldPosition);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                pathSuccess = true;
                break;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        visited.Add(neighbour.worldPosition);

                    }
                }
            }

        }
       
        if(pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
            myPlayer.GetPath(waypoints);
        }
    }

    public void FindPathDijkstra(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos); 

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        visited.Add(startNode.worldPosition);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].gCost < currentNode.gCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                pathSuccess = true;
                break;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        visited.Add(neighbour.worldPosition);

                    }
                }
            }

        }

        if(pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
            myPlayer.GetPath(waypoints);
        }
    }
        
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
       
        List<Vector3> waypoints = new List<Vector3>();

        for(int i = 1; i < path.Count; i++)
        {
            waypoints.Add(path[i].worldPosition);
        }

        Vector3[] waypoints2 = waypoints.ToArray();
        Array.Reverse(waypoints2);
        return waypoints2;

    }

    Vector3[] RetracePathPOV(GameObject startNode, GameObject endNode)
    {
        List<GameObject> path = new List<GameObject>();
        GameObject currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            path.Add(currentNode);
            currentNode = currentNode.GetComponent<POVNode>().parent;
        }

        List<Vector3> waypoints = new List<Vector3>();

        for(int i = 1; i < path.Count; i++)
        {
            waypoints.Add(path[i].transform.position);
        }

        Vector3[] waypoints2 = waypoints.ToArray();
        Array.Reverse(waypoints2);
        return waypoints2;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
            //return (int)Math.Sqrt(2) * dstY + 1 * (dstX - dstY);
        }

        return 14 * dstX + 10 * (dstY - dstX);
        //return (int)Math.Sqrt(2) * dstX + 1 * (dstY - dstX);
    }

    float GetDistancePOV(GameObject nodeA, GameObject nodeB)
    {
        return Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
    }

    public void OnDrawGizmos()
    {
        if(visited2 != null)
        {
            for (int j = 0; j < visited2.Count; j++)
            {
                visited2[j].GetComponent<Renderer>().material.color = new Color(0,0,1);
            }
        }

        if(visited != null)
        {
            for (int i = 0; i < visited.Count; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(visited[i], new Vector3(0.05f, 0.05f, 0.05f));
            }
        }
    }

    public void FindPathDijkstraPOV()
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        GameObject startNode = null;
        GameObject targetNode = null;

        foreach(Transform child in POVNodes)
        {
            if(child.gameObject.GetComponent<POVNode>().amIStart() == true)
            {
                startNode = child.gameObject;
            }
           if(child.gameObject.GetComponent<POVNode>().amITarget() == true)
            {
                targetNode = child.gameObject;
            }
        }
            
        List<GameObject> openSet = new List<GameObject>();
        HashSet<GameObject> closedSet = new HashSet<GameObject>();
        openSet.Add(startNode);
        visited2.Add(startNode);

        while(openSet.Count > 0)
        {
            GameObject currentNode = openSet[0];

            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].GetComponent<POVNode>().gCost < currentNode.GetComponent<POVNode>().gCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode.gameObject.name == targetNode.gameObject.name)
            {
                pathSuccess = true;
                break;
            }

            foreach (GameObject neighbour in currentNode.GetComponent<POVNode>().neighbours)
            {
                if(closedSet.Contains(neighbour))
                {
                    continue;
                }

                float newMovementCostToNeighbour = currentNode.GetComponent<POVNode>().gCost + GetDistancePOV(currentNode, neighbour);

                if(newMovementCostToNeighbour < neighbour.GetComponent<POVNode>().gCost || !openSet.Contains(neighbour))
                {
                    neighbour.GetComponent<POVNode>().gCost = newMovementCostToNeighbour;
                    neighbour.GetComponent<POVNode>().parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        visited2.Add(neighbour);

                    }
                }
            }

        }
        if(pathSuccess)
        {
            waypoints = RetracePathPOV(startNode, targetNode);
            myPlayer.GetPath(waypoints);
        }
    }

    public void FindPathAStarPOV()
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        GameObject startNode = null;
        GameObject targetNode = null;

        foreach(Transform child in POVNodes)
        {
            if(child.gameObject.GetComponent<POVNode>().amIStart() == true)
            {
                startNode = child.gameObject;
            }
            if(child.gameObject.GetComponent<POVNode>().amITarget() == true)
            {
                targetNode = child.gameObject;
            }
        }
            
        List<GameObject> openSet = new List<GameObject>();
        HashSet<GameObject> closedSet = new HashSet<GameObject>();
        openSet.Add(startNode);
        visited2.Add(startNode);

        while(openSet.Count > 0)
        {
            GameObject currentNode = openSet[0];

            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].GetComponent<POVNode>().fCost < currentNode.GetComponent<POVNode>().fCost || openSet[i].GetComponent<POVNode>().fCost == currentNode.GetComponent<POVNode>().fCost && openSet[i].GetComponent<POVNode>().hCost < currentNode.GetComponent<POVNode>().hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode.gameObject.name == targetNode.gameObject.name)
            {
                pathSuccess = true;
                break;
            }

            foreach (GameObject neighbour in currentNode.GetComponent<POVNode>().neighbours)
            {
                if(closedSet.Contains(neighbour))
                {
                    continue;
                }

                float newMovementCostToNeighbour = currentNode.GetComponent<POVNode>().gCost + GetDistancePOV(currentNode, neighbour);

                if(newMovementCostToNeighbour < neighbour.GetComponent<POVNode>().gCost || !openSet.Contains(neighbour))
                {
                    neighbour.GetComponent<POVNode>().gCost = newMovementCostToNeighbour;
                    neighbour.GetComponent<POVNode>().hCost = GetDistancePOV(neighbour, targetNode);
                    neighbour.GetComponent<POVNode>().parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        visited2.Add(neighbour);
                    }
                }
            }

        }
        if(pathSuccess)
        {
            waypoints = RetracePathPOV(startNode, targetNode);
            myPlayer.GetPath(waypoints);
        }
    }
}
