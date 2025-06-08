using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class StateEnemyAttack : State
{
    // Start is called before the first frame update
    EnemyAnimController controller;
    public StateEnemyAttack(IStateMachine stateMachine) : base(stateMachine) {
        controller = (stateMachine.AIBehaviour as EnemyAnimBehavior).Controller;
    }
    public override void OnEnter(IState prev)
    {
        base.OnEnter(prev);
        controller.EnemyAnimator.SetAttack();
    }
    
}

