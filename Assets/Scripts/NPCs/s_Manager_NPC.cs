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
    [SerializeField] SpriteRenderer thisSpriteRenderer;         //For manipulating the direction of the NPC Sprite.

    PlayerInput playerInput;
    InputAction ductTapeAction; //For use with the new input system. Used for detecting if the player has been caught or not while player is touched by NPC CircleCast.

    Vector2 transform2D; //Origin for our Raycast (CircleCast).
    [SerializeField] float circleCastRadius;    //Lets us set the size of the NPC's Raycast, which is used for detecting if NPC can catch the player cheating.

    [SerializeField] bool punishmentEnabled = true; //If enabled and the player cheats, they get punished. This will only be disabled if the player has recently been caught cheating.


    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player"); //Rather than search multiple times, just search once and keep getting from the reference.
        playerController = playerGameObject.GetComponent<PlayerController>();            //Get the player controller (Current test DummYPlayer controller. Replace with real one later.


        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>(); //Gets the Sprite Renderer so that we can minupulate the sprite (in particular, the direction the sprite is facing.

        playerInput = playerController.GetComponent<PlayerInput>(); //Get reference to access the players action list.
        ductTapeAction = playerInput.actions["DuctTape"];           //Get reference to the specific action we wish to monitor. 
                                                                    //Currently null, which is correct because the action "DuctTape" doesn't exist in the playerInput.

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
            Debug.Log("Ping."); //Debug message.

            //Branching action based on the time until it's allowed to walk again.
            if (currentWaitTime > 0.0f)  //If the time is not zero.
            {
                currentWaitTime -= Time.deltaTime;  //Decrement the time by deltaTime..
                CastCircleCast();   //And cast the circlular Raycast.
            }
            else //But if the time is zero.
            {
                currentWaitTime = maxWaitTIme; //reset the timer that it must wait.
                ChangeDestination();    //Set a new destination. This could end up being the same point that it is on, which case it will re-roll on the next frame.
                                        //If the destination is at a different location than the current position, the NPC will walk.
                                        //If not, it's just waiting at the same spot and re-reolls a new destination.
                                            //This has the addition of acting like a second layer of rng. It could stay because the location is repeatedly the same, or it moves.

            }

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

    void CastCircleCast()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform2D, circleCastRadius, transform.right, LayerMask.NameToLayer("Player")); //Add player layer later.

        foreach (RaycastHit2D hit in hits) //This can be removed later. Simply used to tell me if something is being detected.
        {
            Debug.Log("Hit: " + hit.collider.name + "\n"); //Remove me later.
        }

        //Real detection code.
        if(hits.Any<RaycastHit2D>()) //If the player is detected, they are in view, and may now be punished if they hit the Duct Tape Button.
        {

            if (ductTapeAction.IsPressed() && punishmentEnabled == true){ //Replaced "Input.GetKeyDown(KeyCode.Space)" with new input system. SHOULD be correct.
                PunishmentEvent(); //Trigger punishement.
                punishmentEnabled= false; //Bacause the player has been punished, we want to take some time to ensure the player isn't punished repeatedly rapidly.
                                            //So the punishment will be re-enabled once the NPC is moving again, and disabled while waiting at the same spot.
            }
             
            Debug.Log("Hit: " + hits[0].collider.name + "\n");  //No need to go anywhere above the first index, as all indexes will have the same information.

            Debug.Log("Hits size: " +hits.Length);  //Just checking if it was a per pixel thing. It's per object.
        }
        //This code block checks an index of 1 size every frame. So it will be quite accurate to detect the instant the player cheats.
        //Will now impletement a way to stop checking if the cheat until they've moved away.

        //Now needing to implement logic for what happens only when the player makes a specific action, only while the raycast is touching.
        
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
