using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]private PlayerMain _stats;
    private PlayerControl _player;
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
        _player = GetComponent<PlayerControl>();
        _anim = GetComponent<Animator>();
    }
    private int _state;
    private int _currentState;
    private void Update() 
    {
        _state = ChangeState();
        if(_state == _currentState) return;
        _anim.CrossFade(_state, 0, 0);
        _currentState = _state;
    }
    private int ChangeState()
    {
        if(_stats.PlayerHurted) return HURT;
        if(_player.Crouching) return CROUCH;
        if(_player.Climbing) return CLIMB;
        if(!_player.IsGrounded) return _rb.velocity.y >= 0? JUMP: FALL;
        return _rb.velocity.x == 0? IDLE: RUN;

    }
}
    