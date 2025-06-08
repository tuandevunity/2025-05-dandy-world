using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class StatePlayerRun : State
{
    PlayerAnimController controller;
    public StatePlayerRun(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }
    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetRun();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        controller.PlayerAnimator.OnFixedUpdateRun();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
        controller.PlayerAnimator.ExitRun();
    }
}
