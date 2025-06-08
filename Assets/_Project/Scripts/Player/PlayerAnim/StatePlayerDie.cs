using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerDie : State
{
    PlayerAnimController controller;
    public StatePlayerDie(IStateMachine stateMachine) : base(stateMachine)
    {
        controller = (stateMachine.AIBehaviour as PlayerAnimBehavior).Controller;
    }
    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.PlayerAnimator.SetDie();
    }
}
