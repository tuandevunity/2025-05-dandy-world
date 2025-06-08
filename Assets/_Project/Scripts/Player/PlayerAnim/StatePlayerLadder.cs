using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerLadder : State
{
    PlayerAnimController controller;
    public StatePlayerLadder(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }

    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetLadder();
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        controller.PlayerAnimator.OnUpdateLadder();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
        controller.PlayerAnimator.ExitLadder();
    }
}
