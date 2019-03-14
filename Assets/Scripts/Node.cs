using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parent;
    public int cluster;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _cluster)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        cluster = _cluster;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
