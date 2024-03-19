using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]private PlayerMain _stats;
    private Rigidbody2D _rb;
    private Animator _anim;

    public static readonly int CLIMB = Animator.StringToHash("Climb_Animation");
    public static readonly int CROUCH = Animator.StringToHash("Crouch_Animation");
    public static readonly int IDLE = Animator.StringToHash("Idle_Animation");
    public static readonly int JUMP = Animator.StringToHash("Jump_Animation");
    public static readonly int FALL = Animator.StringToHash("Fall_Animation");
    public static readonly int RUN = Animator.StringToHash("Run_Animation");
    public static readonly int HURT = Animator.StringToHash("Hurt_Animation");

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    private int _state;
    private int _currentState;
    private float LockUntil;
    private void Update() 
    {
        _state = ChangeState();
        if(_state == _currentState) return;
        _anim.CrossFade(_state, 0, 0);
        _currentState = _state;
    }
    private int ChangeState()
    {
        if(GameManager.Instance.GlobalTimer < LockUntil) return _state;

        if(_stats.Hurted) return LockState(HURT, 0.3f);
        if(_stats.Crouching) return CROUCH;
        if(_stats.Climbing) return CLIMB;
        if(!_stats.IsGrounded) return _rb.velocity.y >= 0? JUMP: FALL;
        return _rb.velocity.x == 0? IDLE: RUN;

    }

    //Allow certain animation(state) to play though period of time(locktime) 
    private int LockState(int state, float locktime)
    {
        LockUntil = GameManager.Instance.GlobalTimer + locktime;
        return state;
    }
}
    