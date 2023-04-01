using MiniJamGame16.Minigame;
using MiniJamGame16.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class s_Manager_NPC : MonoBehaviour
{
    //This is the script for the Manager NPC. This NPC will walk back and forth, left to right, pausing as they get to each point they are meant to stop at.

    [SerializeField] PlayerController playerController;  //Setup the ability to detect if the player has hit the Duck Tape (Cheat) key/event.
                                                        //This makes it so the player is punished if caught while within the NPC's Circular Raycast.
                                                        //Remember to change from DummyPlayer to Player later!
    [SerializeField] GameObject[] pausePoints; //Setup multiple points where the manager can stop
    [SerializeField] float currentWaitTime;      //The current wait time for the manager. Serialized for convenience.
    [SerializeField] float maxWaitTIme;          //How long the manager can stay stopped for. Serialize for convenience.
    [SerializeField] int currentTargetPoint;   //Tracking which point they are currently targetting. Serialized for convenience.
    [SerializeField] int walkSpeed;            //The movement speed for the manager.
    [SerializeField] float minimumDistance;    //Distance minimum which the manager can be from his point until his actions change.
    private SpriteRenderer thisSpriteRenderer;         //For manipulating the direction of the NPC Sprite.

    Vector2 transform2D; //Origin for our Raycast (CircleCast).
    [SerializeField] float circleCastRadius;    //Lets us set the size of the NPC's Raycast, which is used for detecting if NPC can catch the player cheating.

    [SerializeField] bool punishmentEnabled = true; //If enabled and the player cheats, they get punished. This will only be disabled if the player has recently been caught cheating.

    private void Awake()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player"); //Rather than search multiple times, just search once and keep getting from the reference.

        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(pausePoints[currentTargetPoint].transform.position, transform.position); //We want the distance between the NPC and the targetPoint.
                                                                                                                    //This helps with deciding which direction they will be walking.

        transform2D=  new Vector2(transform.position.x, transform.position.y); //Used for circular raycast and Gizmo location.

        if (minimumDistance < distance) //If the minimumDistance away (due to float precision floating) is less than the distance between the targetPoint and the manager...
        {
            Walk(); //Walk.
        }
        else //But if the distance is less than the minimum distance.
        {
            ChangeDestination();
        }

    }

    void Walk()
    {
        if (transform.position.x < pausePoints[currentTargetPoint].transform.position.x) //NPC is left of destination, moving right
        {
            transform.Translate((Vector3.right) * Time.deltaTime * walkSpeed);  //Move to new location right.
            thisSpriteRenderer.flipX = true;  //Face sprite RIght

        }
        else //NPC is right of destination, moving left.
        {
            transform.Translate((-Vector3.right) * Time.deltaTime * walkSpeed); //Move to the location left.
            thisSpriteRenderer.flipX = false; //Face Sprite Left

        }
    }

    void ChangeDestination()
    {
        int oldTargetPoint = currentTargetPoint; //Copy current target number to oldTarget
        currentTargetPoint = Random.Range(0, pausePoints.Length);   //set current target to a new number.

        if(oldTargetPoint != currentTargetPoint && punishmentEnabled == false) //if the old number and the new number are different & ppunishment is disabled
        {
            punishmentEnabled = true;   //Then re-enable raycast.
        }
    }

    public void DuctTape()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform2D, circleCastRadius, transform.right, LayerMask.NameToLayer("Player")); //Add player layer later.

        foreach (RaycastHit2D hit in hits) //This can be removed later. Simply used to tell me if something is being detected.
        {
            Debug.Log("Hit: " + hit.collider.name + "\n"); //Remove me later.
        }

        if (punishmentEnabled && hits.Any())
        {
            PunishmentEvent(); //Trigger punishement.
            punishmentEnabled = false;
        }
        else
        {
            MinigameManager.Instance.UseFlox();
        }
    }

    private void OnDrawGizmos() //This is just visualizing our CircleCast. No other way to do it. Can be removed from execution later.
    {
        Gizmos.color = Color.yellow;    //Make the gizmo Yellow.
        Gizmos.DrawWireSphere(transform.position, circleCastRadius); //Draws the Gizmo (easiest way to display a sphere raycast).
    }

    void PunishmentEvent() //Do whatever the manager does to punish the player for getting caught cheating here.
    {
        Debug.Log("Punished!\n"); //Just outputting that the player has been punished for now for testing. Works correctly.

        //Thinking ahead, the punishment should be within the player controller or elsewhere. So coded it that if we're referencing something, we cause the punishement elsewhere.
        //playerController.PunishmentForPlayer(); //If we're punishing the player elsewhere instead of here, we need a reference, so this is it.

    }

}
