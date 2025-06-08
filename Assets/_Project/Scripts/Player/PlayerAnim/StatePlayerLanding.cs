using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerLanding : State
{
    PlayerAnimController controller;
    public StatePlayerLanding(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }

    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetLanding();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
    }
}
