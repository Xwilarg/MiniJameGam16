using MiniJamGame16.Minigame;
using MiniJamGame16.Player;
using UnityEngine;

/// <summary>
/// This is the script for the Manager NPC. This NPC will walk back and forth, left to right, pausing as they get to each point they are meant to stop at.
/// If the player is detect and they attempt to cheat, script reaches out and punishes the player.
/// </summary>
public class s_Manager_NPC : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Setup multiple points where the manager can stop")]
    GameObject[] pausePoints;
    
    [SerializeField]
    [Tooltip("How long the manager can stay stopped for. Serialize for convenience")]
    float maxWaitTIme;

    [SerializeField]
    [Tooltip("Tracking which point they are currently targetting. Serialized for convenience")]
    int currentTargetPoint;

    [SerializeField]
    [Tooltip("The movement speed for the manager")]
    int walkSpeed;

    /// <summary>
    /// Distance minimum which the manager can be from his point until his actions change
    /// </summary>
    float minimumDistance = 0.1f;

    /// <summary>
    /// For manipulating the direction of the NPC Sprite
    /// </summary>
    SpriteRenderer _sr;

    [SerializeField]
    [Tooltip("Lets us set the size of the NPC's Raycast, which is used for detecting if NPC can catch the player cheating")]
    float circleCastRadius;

    [SerializeField]
    private PlayerController _pc;

    private Animator _animator;

    private bool _isLookingAtPlayer;

    void Awake()
    {
        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(pausePoints[currentTargetPoint].transform.position, transform.position); //We want the distance between the NPC and the targetPoint.

        if (minimumDistance < distance) //If the minimumDistance away (due to float precision floating) is less than the distance between the targetPoint and the manager...
        {
            _animator.SetBool("IsWalking", true);
            Walk(); //Walk.
        }
        else //But if the distance is less than the minimum distance.
        {
            _animator.SetBool("IsWalking", false);
            ChangeDestination();
        }

    }

    private void Update()
    {
        // Collider2D hits = Physics2D.OverlapCircle(transform.position, circleCastRadius, LayerMask.GetMask("Player")); //Add player layer later.
        // _isLookingAtPlayer = hits != null; // TODO: Broken
    }

    void Walk()
    {
        var isGoingRight = transform.position.x < pausePoints[currentTargetPoint].transform.position.x;
        transform.Translate(Time.deltaTime * walkSpeed * (isGoingRight ? Vector3.right : Vector3.left));
        _sr.flipX = !isGoingRight;
    }

    void ChangeDestination()
    {
        if (currentTargetPoint == pausePoints.Length - 1) currentTargetPoint = 0;
        else currentTargetPoint++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;    //Make the gizmo Yellow.
        Gizmos.DrawWireSphere(transform.position, circleCastRadius); //Draws the Gizmo (easiest way to display a sphere raycast).
    }

    public void UseTape()
    {
        _isLookingAtPlayer =
            (!_sr.flipX && transform.position.x <= _pc.transform.transform.position.x) ||
            (_sr.flipX && transform.position.x >= _pc.transform.transform.position.x);
        if (_isLookingAtPlayer)
        {
            MinigameManager.Instance.IncreaseError();
        }
        else
        {
            MinigameManager.Instance.UseFlox();
        }
    }
}
