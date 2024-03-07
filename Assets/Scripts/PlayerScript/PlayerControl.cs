using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Tilemaps;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using TMPro;
using Unity.VisualScripting;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private PlayerMain _stats;
    [SerializeField]private GameEndedScript _gameEndedScreen;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private FrameInput _frameInput;
    //cached velocity reference before applying the movement
    private Vector2 _frameVelocity;
    private float _horizontal;
    private float _vertical;
    private bool _isFaceRight = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _stats.MaxScore = GameObject.FindGameObjectsWithTag("Item").Length; 
    }

    private void Start()
    {
        _stats.GameFinished = false;
        _stats.PlayerHealth = 3;
        _stats.PlayerScore = 0;
        _stats.Timer = 0;
    }


    private void Update()
    {
        //stop the game if game is finished
        Time.timeScale = _stats.GameFinished? 0: 1;

        //funciton GameEnded(bool) takes true as winning
        if(_stats.PlayerHealth <= 0)
        {
            //losing
            _stats.GameFinished = true;
            _gameEndedScreen.GameEnded(false);
        }
        else if(_stats.PlayerScore == _stats.MaxScore)
        {
            //winning
            _stats.GameFinished = true;;
            _gameEndedScreen.GameEnded(true);
        }

        _stats.Timer += Time.deltaTime;
        GatherInput();
    }


    //function to handle player input
    private void GatherInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump"),
            JumpHeld = Input.GetButton("Jump"),
            CrouchHeld = Input.GetButton("Crouch"),
            ClimbAttempted = Input.GetButton("Vertical"),
            Move = new Vector2(_horizontal,_vertical)
        };

        if(_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpPressed = _stats.Timer;
        }

        Crouching = _frameInput.CrouchHeld && IsGrounded && !Climbing? true: false;

    }
    
    private void FixedUpdate() 
    {
        CollisionCheck();
        
        HandleDirection();
        HandleMotion();
        HandleJump();
        HandleGravity();

        ApplyMovement();

    }

    #region Collision
    public bool IsGrounded;

    private void CollisionCheck()
    {
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.FeetCheckDistance, _stats.groundLayer | _stats.PlatformLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.RoofCheckDistance, _stats.groundLayer);

        //hit a ceiling 
        if(ceilingHit) _frameVelocity.y = Mathf.Min(0,_frameVelocity.y);

        //Land on ground
        if(!IsGrounded && groundHit){
            IsGrounded = true;
        }

        //leave the ground
        if(IsGrounded && !groundHit){
            IsGrounded = false;
            _timeLeftGround = _stats.Timer;
        }
    }
    #endregion

    #region Direction
    private void HandleDirection()
    {
        if(_isFaceRight &&  _horizontal< 0f || !_isFaceRight && _horizontal > 0f)
        {
            _isFaceRight = !_isFaceRight;
            transform.localScale = new Vector3(transform.localScale.x* -1, transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    #region Motion(Horizontal + Climbing)
    private float _tarSpeed;
    public bool Crouching;
    public bool Climbing;
    private void OnTriggerStay2D(Collider2D _ladder)
    {   
        //Find the ladder
        if(!_ladder.gameObject.CompareTag("Ladder")) return;

        //Catch the ladder
        if(_frameInput.ClimbAttempted)
        {
            Climbing = true;
            //Align player to the ladder
            transform.position = new Vector3(_ladder.bounds.center.x, transform.position.y, transform.position.z);
        }    
    }
    //Exit the ladder
    private void OnTriggerExit2D(Collider2D _ladder) {
        Climbing = false;
    }

    private void HandleMotion()
    {
        //holds player back while getting hurt
        if(_stats.PlayerHurted) return;
        
        if(Climbing)
        {
            _frameVelocity.x = 0;
            _frameVelocity.y = _vertical * _stats.ClimbSpeed;

            //blocking the horizontal motion if climbing
            return;
        }
        //Decceleration
        if(_frameInput.Move.x == 0)
        {
            _tarSpeed = 0;
            float deccerlation = IsGrounded? _stats.GroundDecceleration : _stats.AirDecceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _tarSpeed, deccerlation);
            
        }
        //Acceleration
        else
        {
            //Crouching
            //IsGrounded check to prevent crouching mid-air
            _tarSpeed = IsGrounded && Crouching? _stats.TopSpeed * _stats.CrouchSpeedModifier : _stats.TopSpeed;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _horizontal*_tarSpeed, _stats.Acceleration);
        }
    }
    #endregion
    
    #region Jump
    private bool _jumpToConsume;
    private bool _endedJumpEarly;
    private float _timeLeftGround = float.MinValue;
    private float _timeJumpPressed = float.MinValue;

    private bool _canUseCoyote => _stats.Timer < _timeLeftGround + _stats.CoyoteTime;
    private bool _canUseBufferJump => _stats.Timer < _timeJumpPressed + _stats.BufferTime;
    private void HandleJump()
    {   
        if(_stats.BounceOffHead || _stats.PlayerHurted) StartCoroutine(ForcedJump());
        if(!_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;
        if(!_jumpToConsume && !_canUseBufferJump) return;
        if(IsGrounded || _canUseCoyote || Climbing) ExecuteJump();
        _jumpToConsume = false;
        
        
    }
    private IEnumerator ForcedJump()
    {
        if(_stats.BounceOffHead) 
        {
            _frameVelocity.y = _stats.StompBounceForce;
            _stats.BounceOffHead = false;
        }
        if(_stats.PlayerHurted) 
        {

            var oppositeFacing = _isFaceRight? -1: 1;
            _frameVelocity.x = _stats.KnockBackForce * oppositeFacing;
            _frameVelocity.y = _stats.KnockBackForce;
            yield return new WaitForSeconds(0.3f);
            _stats.PlayerHurted = false;
        }
    }
    private void ExecuteJump()
    {
        //reset the jump data 
        _timeJumpPressed = 0;
        _endedJumpEarly = false;
        //jump off the ladder
        Climbing = false;

        _frameVelocity.y = _stats.JumpForce;
    }
    #endregion

    #region Gravity
    private void HandleGravity()
    {
        //disable gravity when climbing
        if(Climbing) return;

        if(IsGrounded && _frameVelocity.y <= 0)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var MidAirGravity = _stats.FallGravity;
            if(_endedJumpEarly) MidAirGravity *= _stats.EndedJumpEarlyModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, MidAirGravity * Time.fixedDeltaTime);
        }
    }
    #endregion

    private void ApplyMovement() => _rb.velocity = _frameVelocity;



}
public struct FrameInput{
    public bool JumpDown;
    public bool JumpHeld;
    public bool CrouchHeld;
    public bool ClimbAttempted;
    public Vector2 Move;
}