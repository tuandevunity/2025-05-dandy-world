using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerWalk : State
{
    PlayerAnimController controller;
    public StatePlayerWalk(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }
    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetWalk();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        controller.PlayerAnimator.OnFixedUpdateWalk();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
        controller.PlayerAnimator.ExitWalk();
    }

}
