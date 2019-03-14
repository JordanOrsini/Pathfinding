using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControllerScript : MonoBehaviour 
{
    public AudioSource startSound;
    AudioSource myStartSound;

    public Transform playerTransform;
    public Transform targetTransform;

    public GameObject startText;
    float currentTime;

    int randomNumber;
    int targetNumber;

    Vector3 pos1;
    Vector3 pos2;
    Vector3 pos3;
    Vector3 pos4;

    public GameObject POVNode1;
    public GameObject POVNode2;
    public GameObject POVNode3;
    public GameObject POVNode4;

    void Awake()
    {
        pos1 = new Vector3(-2.04f, 0f, -1.74f);
        pos2 = new Vector3(2.13f, 0f, -1.42f);
        pos3 = new Vector3(-2.17f, 0f, 1.36f);
        pos4 = new Vector3(1.815f, 0f, 0.321f);

        randomNumber = Random.Range(1,5);

        //TEST
        //randomNumber = 2;

        if(randomNumber == 1)
        {
            playerTransform.transform.position = pos1;
            POVNode1.gameObject.GetComponent<POVNode>().setStartNode();

            targetNumber = Random.Range(1,4);

            if(targetNumber == 1)
            {
                targetTransform.transform.position = pos2;
                POVNode2.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 2)
            {
                targetTransform.transform.position = pos3;
                POVNode3.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 3)
            {
                targetTransform.transform.position = pos4;
                POVNode4.gameObject.GetComponent<POVNode>().setTargetNode();
            }
        }
        if(randomNumber == 2)
        {
            playerTransform.transform.position = pos2;
            playerTransform.localEulerAngles = new Vector3(0,180,0);
            POVNode2.gameObject.GetComponent<POVNode>().setStartNode();

            targetNumber = Random.Range(1,4);

            //TEST
            //targetNumber = 2;

            if(targetNumber == 1)
            {
                targetTransform.transform.position = pos1;
                POVNode1.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 2)
            {
                targetTransform.transform.position = pos3;
                POVNode3.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 3)
            {
                targetTransform.transform.position = pos4;
                POVNode4.gameObject.GetComponent<POVNode>().setTargetNode();
            }
        }
        if(randomNumber == 3)
        {
            playerTransform.transform.position = pos3;
            playerTransform.localEulerAngles = new Vector3(0,0,0);
            POVNode3.gameObject.GetComponent<POVNode>().setStartNode();

            targetNumber = Random.Range(1,4);

            if(targetNumber == 1)
            {
                targetTransform.transform.position = pos1;
                POVNode1.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 2)
            {
                targetTransform.transform.position = pos2;
                POVNode2.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 3)
            {
                targetTransform.transform.position = pos4;
                POVNode4.gameObject.GetComponent<POVNode>().setTargetNode();
            }
        }
        if(randomNumber == 4)
        {
            playerTransform.transform.position = pos4;
            POVNode4.gameObject.GetComponent<POVNode>().setStartNode();

            targetNumber = Random.Range(1,4);

            if(targetNumber == 1)
            {
                targetTransform.transform.position = pos1;
                POVNode1.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 2)
            {
                targetTransform.transform.position = pos2;
                POVNode2.gameObject.GetComponent<POVNode>().setTargetNode();
            }
            if(targetNumber == 3)
            {
                targetTransform.transform.position = pos3;
                POVNode3.gameObject.GetComponent<POVNode>().setTargetNode();
            }
        }

        playerTransform.gameObject.SetActive(true);
        targetTransform.gameObject.SetActive(true);
    }

    void Start () 
    {
        myStartSound = startSound.GetComponent<AudioSource>();
        Instantiate(myStartSound);
        startText.SetActive(true);
        currentTime = Time.time;
	}
	
	void Update () 
    {
        if(Time.time > currentTime + 1.5f)
        {
            startText.SetActive(false);
        }
	}
}
