using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Manager_NPC : MonoBehaviour
{
    //This is the script for the Manager NPC. This NPC will walk back and forth, left to right, pausing as they get to each point they are meant to stop at.

    [SerializeField] GameObject[] pausePoints; //Setup multiple points where the manager can stop
    [SerializeField] int currentWaitTime;              //The current wait time for the manager. Serialized for convenience.
    [SerializeField] int maxWaitTIme;                  //How long the manager can stay stopped for. Serialize for convenience.
    [SerializeField] int currentTargetPoint;          //Tracking which point they are currently targetting. Serialized for convenience.
    [SerializeField] float walkSpeed;            //The movement speed for the manager.


    // Start is called before the first frame update
    void Start()
    {
        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        Debug.Log(currentTargetPoint);
        Debug.Log(pausePoints.Length);  //Sanity check, remove later.
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < pausePoints[currentTargetPoint].transform.position.x)          //Assuming left of current target point.
        {
            this.transform.position = new Vector3(transform.position.x + walkSpeed, transform.position.y, transform.position.z);

        }
        else if( this.transform.position.x > pausePoints[currentTargetPoint].transform.position.x)      //Assuming right of current target point.
        {
            this.transform.position = new Vector3(transform.position.x - walkSpeed, transform.position.y, transform.position.z);

        }
        else //Assuming on target point.
        {
            //Doing nothing for now.
            Debug.Log("At Destintion.");

            if(currentWaitTime != 0)
            {
                currentWaitTime -= 1;
            }
            else
            {

            }

        }
    }
}
