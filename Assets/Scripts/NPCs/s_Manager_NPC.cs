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
    //If the player is detect and they attempt to cheat, script reaches out and punishes the player.

    [SerializeField] GameObject[] pausePoints; //Setup multiple points where the manager can stop
    float currentWaitTime;      //The current wait time for the manager. Serialized for convenience.
    [SerializeField] float maxWaitTIme;          //How long the manager can stay stopped for. Serialize for convenience.
    int currentTargetPoint;   //Tracking which point they are currently targetting. Serialized for convenience.
    [SerializeField] int walkSpeed;            //The movement speed for the manager.
    float minimumDistance = 0.1f;    //Distance minimum which the manager can be from his point until his actions change.
    SpriteRenderer thisSpriteRenderer;         //For manipulating the direction of the NPC Sprite.

    Vector2 transform2D; //Origin for our Raycast (CircleCast).
    [SerializeField] float circleCastRadius;    //Lets us set the size of the NPC's Raycast, which is used for detecting if NPC can catch the player cheating.

    [SerializeField] bool punishmentEnabled = true; //If enabled and the player cheats, they get punished. This will only be disabled if the player has recently been caught cheating.


    // Start is called before the first frame update
    void Awake()
    {

        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>(); //Gets the Sprite Renderer so that we can minupulate the sprite (in particular, the direction the sprite is facing.

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(pausePoints[currentTargetPoint].transform.position, transform.position); //We want the distance between the NPC and the targetPoint.
                                                                                                                    //This helps with deciding which direction they will be walking.
        transform2D =  new Vector2(transform.position.x, transform.position.y); //Used for circular raycast and Gizmo location.

        if (minimumDistance < distance) //If the minimumDistance away (due to float precision floating) is less than the distance between the targetPoint and the manager...
        {
            Walk(); //Walk.
        }
        else //But if the distance is less than the minimum distance.
        {
            //Branching action based on the time until it's allowed to walk again.
            if (currentWaitTime > 0.0f)
            {
                currentWaitTime -= Time.deltaTime;
            }
            else //But if the time is zero.
            {
                currentWaitTime = maxWaitTIme; //reset the timer that it must wait.
                ChangeDestination();    //Set a new destination. This could end up being the same point that it is on, which case it will re-roll on the next frame.

            }

        }

        CastCircleCast(); //Should always be casting, not just while it's waiting.

    }

    void Walk()
    {
        if (transform.position.x < pausePoints[currentTargetPoint].transform.position.x) //NPC is left of destination, moving right
        {
            transform.Translate((Vector3.right) * Time.deltaTime * walkSpeed);  //Move to new location right.
            thisSpriteRenderer.flipX = false;  //Face sprite RIght

        }
        else //NPC is right of destination, moving left.
        {
            transform.Translate((-Vector3.right) * Time.deltaTime * walkSpeed); //Move to the location left.
            thisSpriteRenderer.flipX = true; //Face Sprite Left

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
        Collider2D hits = Physics2D.OverlapCircle(transform2D, circleCastRadius, LayerMask.GetMask("Player")); //Add player layer later.

        if (hits != null && Input.GetKeyDown(KeyCode.Space) && punishmentEnabled == true){ //Replaced with old input for testing.
            punishmentEnabled = false;
            PunishmentEvent(); //Trigger punishement.
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

        MinigameManager.Instance.UseFlox();
    }

}
