using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerAttack : State
{
    PlayerAnimController controller;
    public StatePlayerAttack(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }

    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetAttack();
    }
}
