using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IBodyCollision, IWeakPointCollision
{
    [SerializeField] private PlayerMain _playerStats;
    [SerializeField] private EnemyStats _enemyStats;
    private Animator _anim;
    private Rigidbody2D _rb;
    private Vector2 _frameVelocity;
    private Collider2D _bodyCollider;
    private Collider2D _groundDetector;
    private float _spawnPos;
    private float _currentPos;
    private float _roamRange;

    // Start is called before the first frame update
    private void Awake()
    {   
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _bodyCollider = GetComponent<BoxCollider2D>();
        _groundDetector = GetComponentInChildren<BoxCollider2D>();
    }


    private void Start() 
    {
        _enemyStats.Health = 1;
        _enemyStats.IsDefeated = false;
        _facingRight = _enemyStats.InitialFaceRight;
        //get initial position of the mob
        _spawnPos = transform.position.x;
        _roamRange = _enemyStats.RoamingRange*2;
    }


    private float _currentTime;
    private void Update()
    {
        _currentTime = _playerStats.Timer;
    }


    private void FixedUpdate()
    {
        HandleDirection();
        HandleRoaming();
        ApplyMovement();
    }


    #region Roaming

    //dir used to convert the bool of _facingRight into vector -1 or 1
    private int _dir;
    private bool _facingRight;
    private void HandleRoaming()
    {
        _dir = _facingRight? 1: -1;
        _frameVelocity = new Vector2(_dir * _enemyStats.MoveSpeed, -1.5f);
        _currentPos = transform.position.x;
    }

    //_turnTIme used to store the time of direction changed
    private float _turnTime = float.MinValue;
    private bool _shouldTurn = false;
    private void HandleDirection()
    {
        //Use a time threshold to guard the collision from happening too quick
        if(_currentTime < _turnTime + _enemyStats.DirChangeThreshold) return;

        //Three conditions to trigger face change:
        //1.reaching maximum roam radius
        //2.hit a wall
        //3.come to a cliff
        PositionCheck();
        WallCheck();
        CliffCheck();

        if(_shouldTurn) ChangeFacing();
    }

    private void PositionCheck()
    {
        if(_currentPos > _spawnPos + _roamRange || _currentPos < _spawnPos - _roamRange) _shouldTurn = true;
    }


    private void WallCheck()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(_bodyCollider.bounds.center, new Vector2(_dir,0), _enemyStats.RayCastDistance, _enemyStats.GroundLayer);
        if(wallHit.collider != null) _shouldTurn = true;

    }


    private void CliffCheck()
    {
        RaycastHit2D groundDetect = Physics2D.Raycast(_groundDetector.bounds.center, Vector2.down, _enemyStats.RayCastDistance, _enemyStats.GroundLayer);
        if(groundDetect.collider == null) _shouldTurn = true;
    }


    private void ChangeFacing()
    {
        _turnTime = _currentTime;
        _shouldTurn = false;
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    

    private void ApplyMovement() 
    {
        _rb.velocity = _frameVelocity;
    }


    #endregion
    
    #region interface
    public void BodyHit()
    {
        _playerStats.Hurted = true;
        _playerStats.PlayerHealth -= _enemyStats.MobDmg;
    }


    public void WeakPointHit()
    {
        _playerStats.BounceOffHead = true;
        _enemyStats.Health -= _playerStats.PlayerDamge;
        if(_enemyStats.Health <= 0) 
        {
            _enemyStats.IsDefeated = true;
            StartCoroutine(Defeated());
        }
    }
    #endregion
    

    //use coroutine to delay object destruciton, allow animation to play through
    IEnumerator Defeated()
    {
        _bodyCollider.enabled = false;
        _anim.Play("Mob_Defeated");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
