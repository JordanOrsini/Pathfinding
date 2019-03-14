using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVNode : MonoBehaviour 
{
    public Vector3 worldPosition;

    public float gCost;
    public float hCost;

    public GameObject parent;
    public int cluster;

    public bool isStartNode;
    public bool isTargetNode;

    public List<GameObject> neighbours = new List<GameObject>();

    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public bool amIStart()
    {
        return isStartNode;
    }

    public bool amITarget()
    {
        return isTargetNode;
    }

    public void setStartNode()
    {
        isStartNode = true;
    }

    public void setTargetNode()
    {
        isTargetNode = true;
    }

    public void UsingClusters()
    {
        if(cluster == 1)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(cluster == 2)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if(cluster == 3)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.95f, 0.58f, 0.26f, 1f);
        }
        else if(cluster == 4)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if(cluster == 5)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.35f, 0.13f, 0.55f, 1f);
        }
    }
}
