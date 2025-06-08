using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    // Start is called before the first frame update
    StateEnemyIdle stateIdle;
    StateEnemyWalk stateWalk;
    StateEnemyAttack stateAttack;

    public EnemyStateMachine(AIBehaviour aIBehaviour) : base(aIBehaviour)
    {
        InitStates();
    }

    void InitStates() 
    {
        stateIdle = new StateEnemyIdle(this);
        stateAttack = new StateEnemyAttack(this);
        stateWalk = new StateEnemyWalk(this);

        this.AddTransition(new Transition(stateIdle, stateAttack));
        this.AddTransition(new Transition(stateIdle, stateWalk));

        this.AddTransition(new Transition(stateAttack, stateIdle));
        this.AddTransition(new Transition(stateAttack, stateWalk));

        this.AddTransition(new Transition(stateWalk, stateAttack));
        this.AddTransition(new Transition(stateWalk, stateIdle));
    }
}
