using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_Manager_NPC : MonoBehaviour
{
    //This is the script for the Manager NPC. This NPC will walk back and forth, left to right, pausing as they get to each point they are meant to stop at.

    [SerializeField] GameObject[] pausePoints; //Setup multiple points where the manager can stop
    [SerializeField] int currentWaitTime;              //The current wait time for the manager. Serialized for convenience.
    [SerializeField] int maxWaitTIme;                  //How long the manager can stay stopped for. Serialize for convenience.
    [SerializeField] int currentTargetPoint;          //Tracking which point they are currently targetting. Serialized for convenience.
    [SerializeField] int walkSpeed;            //The movement speed for the manager.
    [SerializeField] float minimumDistance;     //Distance minimum which the manager can be from his point until his actions change.


    // Start is called before the first frame update
    void Start()
    {
        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        Debug.Log(currentTargetPoint);
        Debug.Log(pausePoints.Length);  //Sanity check, remove later.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(pausePoints[currentTargetPoint].transform.position, transform.position);

        if (minimumDistance < distance)
        {
            Walk();
        }
        else
        {
            Debug.Log("Ping.");

            if(currentWaitTime != 0)
            {
                currentWaitTime--;
            }
            else
            {
                currentWaitTime = maxWaitTIme;
                ChangeDestination();

            }


        }

    }

    void Walk()
    {
        if(transform.position.x < pausePoints[currentTargetPoint].transform.position.x) //NPC is left of destination
        {
            transform.Translate((Vector3.right) * Time.deltaTime * walkSpeed);
            //Face sprite RIght

        }
        else //NPC is right of destination
        {
            transform.Translate((-Vector3.right) * Time.deltaTime * walkSpeed);
            //Face Sprite Left

        }
    }

    void ChangeDestination()
    {
            currentTargetPoint = Random.Range(0, pausePoints.Length);
    }

}
