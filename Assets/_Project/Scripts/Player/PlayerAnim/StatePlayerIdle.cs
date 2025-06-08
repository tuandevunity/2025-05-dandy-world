using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class StatePlayerIdle : State
{
    PlayerAnimController controller;
    public StatePlayerIdle(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }

    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetIdle();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
        controller.PlayerAnimator.ExitIdle();
    }
}
