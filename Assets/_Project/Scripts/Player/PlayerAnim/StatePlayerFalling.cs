using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerFalling : State
{
    PlayerAnimController controller;
    public StatePlayerFalling(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }

    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetFalling();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
    }
}
