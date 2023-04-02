using MiniJamGame16.Minigame;
using MiniJamGame16.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    [SerializeField]
    private Image _srEye;

    [SerializeField]
    private Sprite _eyeClosed, _eyeOpened;

    [SerializeField]

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

    [SerializeField]
    private Volume _globalVolume;

    [SerializeField]
    [ColorUsage(true, true)]
    private Color _sceneColor, _sceneSpottedColor;

    void Awake()
    {
        currentTargetPoint = Random.Range(0, pausePoints.Length);     //At level load, randomize which point the manager is going to move to.
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        if (_globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            _colorAdjustments = colorAdjustments;
        }
        if (_globalVolume.profile.TryGet(out ChromaticAberration chromaticAberration))
        {
            _chromaticAberration = chromaticAberration;
        }
    }

    void FixedUpdate()
    {
        if (MinigameManager.Instance.IsInspectionOn)
        {
            _animator.SetBool("IsWalking", false);
            _srEye.sprite = _eyeOpened;
            _isLookingAtPlayer = true;
            return;
        }
        _animator.SetBool("IsWalking", true);

        float distance = Vector3.Distance(pausePoints[currentTargetPoint].transform.position, transform.position); //We want the distance between the NPC and the targetPoint.

        if (minimumDistance < distance) //If the minimumDistance away (due to float precision floating) is less than the distance between the targetPoint and the manager...
        {
            Walk(); //Walk.
        }
        else //But if the distance is less than the minimum distance.
        {
            ChangeDestination();
        }
        _isLookingAtPlayer =
            (!_sr.flipX && transform.position.x <= _pc.transform.transform.position.x) ||
            (_sr.flipX && transform.position.x >= _pc.transform.transform.position.x);
        _srEye.sprite = _isLookingAtPlayer ? _eyeOpened : _eyeClosed;

    }

    private int _state = 0;

    private float spottedColorIntensity = 0f;
    private ColorAdjustments _colorAdjustments;
    private ChromaticAberration _chromaticAberration;

    private void Update()
    {
        if (_state != 0)
        {
            if (_state == 1)
            {
                spottedColorIntensity += Time.deltaTime * 5f;
                if (spottedColorIntensity >= 1f) _state = 2;
            }
            else
            {
                spottedColorIntensity -= Time.deltaTime * 5f;
                if (spottedColorIntensity <= 0f) _state = 0;
            }
            _colorAdjustments.colorFilter.Interp(_sceneColor, _sceneSpottedColor, spottedColorIntensity);
            _chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, spottedColorIntensity);
        }
        
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
        if (_isLookingAtPlayer)
        {
            MinigameManager.Instance.IncreaseError();
            _state = 1;
        }
        else
        {
            MinigameManager.Instance.UseFlox();
        }
    }
}
