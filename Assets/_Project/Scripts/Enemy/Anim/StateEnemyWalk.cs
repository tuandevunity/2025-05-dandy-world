using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class StateEnemyWalk : State
{
    // Start is called before the first frame update
    EnemyAnimController controller;
    public StateEnemyWalk(IStateMachine stateMachine) : base(stateMachine) {
        controller = (stateMachine.AIBehaviour as EnemyAnimBehavior).Controller;
    }
    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.EnemyAnimator.SetWalk();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
        controller.EnemyAnimator.ExitWalk();
    }
}