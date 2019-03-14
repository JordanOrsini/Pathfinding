using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour 
{
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public LayerMask cluster1Mask;
    public LayerMask cluster2Mask;
    public LayerMask cluster3Mask;
    public LayerMask cluster4Mask;
    public LayerMask cluster5Mask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    public bool drawClusters;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    void Awake()
    {
        drawClusters = false;
        displayGridGizmos = false;
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                if(Physics.CheckSphere(worldPoint, nodeRadius, cluster1Mask))
                {
                    grid[x,y] = new Node(walkable, worldPoint, x, y, 1);
                }
                else if(Physics.CheckSphere(worldPoint, nodeRadius, cluster2Mask))
                {
                    grid[x,y] = new Node(walkable, worldPoint, x, y, 2);
                }
                else if(Physics.CheckSphere(worldPoint, nodeRadius, cluster3Mask))
                {
                    grid[x,y] = new Node(walkable, worldPoint, x, y, 3);
                }
                else if(Physics.CheckSphere(worldPoint, nodeRadius, cluster4Mask))
                {
                    grid[x,y] = new Node(walkable, worldPoint, x, y, 4);
                }
                else if(Physics.CheckSphere(worldPoint, nodeRadius, cluster5Mask))
                {
                    grid[x,y] = new Node(walkable, worldPoint, x, y, 5);
                }
                else
                {
                    grid[x,y] = new Node(walkable, worldPoint, x, y, 0);
                }
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
       
        return grid[x,y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null && displayGridGizmos)
        {
            foreach(Node n in grid)
            {
                if(n.walkable == true)
                {
                    Gizmos.color = Color.white;
                }
                if(n.cluster == 1 && drawClusters == true)
                {
                    Gizmos.color = Color.yellow;
                }
                if(n.cluster == 2 && drawClusters == true)
                {
                    Gizmos.color = Color.magenta;
                }
                if(n.cluster == 3 && drawClusters == true)
                {
                    Gizmos.color = new Color(0.95f, 0.58f, 0.26f, 1f);
                }
                if(n.cluster == 4 && drawClusters == true)
                {
                    Gizmos.color = Color.cyan;
                }
                if(n.cluster == 5 && drawClusters == true)
                {
                    Gizmos.color = new Color(0.35f, 0.13f, 0.55f, 1f);
                }
                if(n.walkable == false)
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawCube(n.worldPosition, new Vector3(0.05f, 0.05f, 0.05f));
            }
        }
    }

    public void turnOnGrid()
    {
        displayGridGizmos = true;
    }

    public void turnOnClusters()
    {
        drawClusters = true;
    }
}
