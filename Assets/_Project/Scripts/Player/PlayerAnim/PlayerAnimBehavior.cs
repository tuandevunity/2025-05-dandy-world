using System.Diagnostics;
using FSM;

public class PlayerAnimBehavior : AIBehaviour
{
    public PlayerAnimController Controller { get; private set; }
    public PlayerState PlayerState { get; set; }
    public override void Init()
    {
        Controller = GetComponent<PlayerAnimController>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StateMachine = new PlayerStateMachine(this);
        StateMachine.ToState<StatePlayerIdle>();
        PlayerState = PlayerState.Idle;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        switch(PlayerState)
        {
            case PlayerState.Idle:
                PlayerIdle();
                break;
            case PlayerState.Run:
                PlayerRun();
                break;
            case PlayerState.Walk:
                PlayerWalk();
                break;
            case PlayerState.Jump:
                PlayerJump();
                
                break;
            case PlayerState.Falling:
                PlayerFalling();
                break;
            case PlayerState.Landing:
                PlayerLanding();
                break;
            case PlayerState.Ladder:
                PlayerLadder();
                break;
            case PlayerState.Jetpack:
                PlayerJetpack();
                break;
            case PlayerState.Die:
                PlayerDie();
                break;
            case PlayerState.Attack:
                PlayerAttack();
                break;
        }
    }
    public void SetPlayerState(PlayerState state)
    {
        PlayerState = state;
    }
    public void SetStateNotLanding(PlayerState state)
    {
        if(PlayerState != PlayerState.Landing)
            PlayerState = state;
    }
    public void SetIdleWhenLanding() //Trigger Not Delete
    {
        PlayerState = PlayerState.Idle;
    }
    public void PlayerIdle()
    {
        StateMachine.ToState<StatePlayerIdle>();
    }
    public void PlayerWalk()
    {
        StateMachine.ToState<StatePlayerWalk>();
    }
    public void PlayerRun()
    {
        StateMachine.ToState<StatePlayerRun>();
    }
    public void PlayerJump()
    {
        StateMachine.ToState<StatePlayerJump>();
    }
    public void PlayerFalling()
    {
        StateMachine.ToState<StatePlayerFalling>();
    }
    public void PlayerLanding()
    {
        StateMachine.ToState<StatePlayerLanding>();
    }
    public void PlayerLadder()
    {
        StateMachine.ToState<StatePlayerLadder>();
    }
    public void PlayerJetpack()
    {
        StateMachine.ToState<StatePlayerJetpack>();
    }
    public void PlayerDie()
    {
        StateMachine.ToState<StatePlayerDie>();
    }
    public void PlayerAttack()
    {
        StateMachine.ToState<StatePlayerAttack>();
    }
}
public enum PlayerState
{
    None,
    Idle,
    Walk,
    Run,
    Jump,
    Falling,
    Landing,
    Ladder,
    Jetpack,
    Die,
    Attack,
}
