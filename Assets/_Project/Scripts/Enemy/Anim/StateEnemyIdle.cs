using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class StateEnemyIdle : State
{
    // Start is called before the first frame update
    EnemyAnimController controller;
    public StateEnemyIdle(IStateMachine stateMachine) : base(stateMachine) {
        controller = (stateMachine.AIBehaviour as EnemyAnimBehavior).Controller;
    }
    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.EnemyAnimator.SetIdle();
    }
    public override void OnExit(IState next)
    {
        base.OnExit(next);
        controller.EnemyAnimator.ExitIdle();
    }
}


