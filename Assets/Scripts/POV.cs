using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POV : MonoBehaviour 
{
    public Transform POVNodes;

    List<GameObject> myPOVList;

    public bool drawConnections;

    public GameObject clusters;


    void Start()
    {
        drawConnections = false;
        myPOVList = new List<GameObject>();
    }

    public void turnOnConnections()
    {
        clusters.SetActive(false);
        drawConnections = true;
        RaycastHit rayHit = new RaycastHit();

        foreach(Transform child in POVNodes)
        {
            myPOVList.Add(child.gameObject);
        }

        foreach (GameObject n in myPOVList)
        {
            foreach (GameObject n2 in myPOVList)
            {
                if(n2.gameObject.name == n.gameObject.name)
                {
                    continue;
                }
                if(drawConnections == true )
                {
                    Ray ray = new Ray(n.transform.position, n2.transform.position - n.transform.position);

                    if (Physics.Raycast(ray,out rayHit) && rayHit.transform == n2.gameObject.transform)
                    {
                        n.GetComponent<POVNode>().neighbours.Add(n2);
                        Debug.DrawRay(n.transform.position, n2.transform.position - n.transform.position, Color.grey, 180f);
                    }

                }
            }
        }
    }
}
