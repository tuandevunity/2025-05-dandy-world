using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimBehavior : AIBehaviour
{
    public EnemyAnimController Controller { get; private set; }
    public EnemyState enemyState { get; set; }

    public override void Init()
    {
        Controller = GetComponent<EnemyAnimController>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StateMachine = new EnemyStateMachine(this);
        StateMachine.ToState<StateEnemyIdle>();
        enemyState = EnemyState.Idle;
    }

    // Start is called before the first frame update
    public override void OnUpdate() 
    {
        base.OnUpdate();
        switch(enemyState)
        {
            case EnemyState.Idle:
                EnemyIdle();
                break;
            
            case EnemyState.Walk:
                EnemyWalk();
                break;
            
            case EnemyState.Attack:
                EnemyAttack();
                break;
        }
    }

    private void EnemyAttack()
    {
        StateMachine.ToState<StateEnemyAttack>();
    }

    private void EnemyWalk()
    {
        StateMachine.ToState<StateEnemyWalk>();
    }

    private void EnemyIdle()
    {
        StateMachine.ToState<StateEnemyIdle>();
    }

    public void SetEnemyState(EnemyState state)
    {
        enemyState = state;
    }

    public enum EnemyState 
    {   
        None,
        Idle,
        Attack,
        Walk
    }
}
