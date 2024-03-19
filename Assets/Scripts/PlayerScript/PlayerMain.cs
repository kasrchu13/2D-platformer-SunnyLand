using System.Buffers.Text;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMain : ScriptableObject
{
    #region Condition Checking Related
    [Tooltip("Detection distance for gound check")]
    public float FeetCheckDistance = 0.3f;

    [Tooltip("Detection distance for roof check")]
    public float RoofCheckDistance = 0.3f;

    [Tooltip("LayerMask to cause the feet and roof collision check")]
    public LayerMask groundLayer;
    public LayerMask PlatformLayer;
    #endregion


    #region Player Movement Stats
    [Header("Movement")][Tooltip("Maximum speed of player")]
    public float TopSpeed = 10;

    [Tooltip("Rate of player reaching maximum speed")]
    public float Acceleration = 100;

    [Tooltip("Rate of player comes to stop on ground")]
    public float GroundDecceleration = 10;

    [Tooltip("Rate of player comes to stop on air ")]
    public float AirDecceleration = 20;

    [Tooltip("Speed multiplier for crouching")]
    public float CrouchSpeedModifier = 0.8f;
    public float ClimbSpeed = 10;

    [Header("Jump")][Tooltip("Jump power of the player")]
    public float JumpForce = 5f;

    [Tooltip("Cutting of the jump height if jump button released early")]
    public float EndedJumpEarlyModifier = 1.8f;

    [Tooltip("Constant force acting when grounded, helps for slopes")]
    public float GroundingForce = -1.5f;

    [Tooltip("Prevent falling without control")]
    public float MaxFallSpeed = 10f;

    [Tooltip("Gravity on falling")]
    public float FallGravity = 40f;

    [Tooltip("Tolerated time for ledge jumping")]
    public float CoyoteTime = 0.1f;

    [Tooltip("Time allowed jump input before hitting ground")]
    public float BufferTime = 0.2f;
    #endregion


    #region Player Interaction Stats
    [Header("Interaction Stats")]
    public static int PlayerInitialHealth = 3;
    public static int PlayerinitialScore = 0;
    [Tooltip("Damage deal upon stomping")]
    public static int PlayerDamge = 1;
    [Tooltip("Bounceiness when stomping on enemy")]
    public float StompBounceForce = 2;
    [Tooltip("Push back force when collided with enemy")]
    public float KnockBackForce = 2; 
    #endregion

    #region Player state
    [Header("Player state")]
    public bool Crouching = false;
    public bool Climbing = false;
    public bool IsGrounded = false;
    public bool BounceOffHead = false;
    public bool Hurted = false;
    #endregion

}

    

    