using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class PlayerStateMachine : StateMachine
{
    StatePlayerIdle stateIdle;
    StatePlayerWalk stateWalk;
    StatePlayerRun stateRun;
    StatePlayerFalling stateFalling;
    StatePlayerJump stateJump;
    StatePlayerLanding stateLanding;
    StatePlayerLadder stateLadder;
    StatePlayerJetpack stateJetpack;
    StatePlayerDie stateDie;
    StatePlayerAttack stateAttack;
    public PlayerStateMachine(AIBehaviour aIBehaviour) : base(aIBehaviour)
    {
        InitStates();
    }
    void InitStates()
    {
        stateIdle = new StatePlayerIdle(this);
        stateWalk = new StatePlayerWalk(this);
        stateRun = new StatePlayerRun(this);
        stateFalling = new StatePlayerFalling(this);
        stateJump = new StatePlayerJump(this);
        stateLanding = new StatePlayerLanding(this);
        stateLadder = new StatePlayerLadder(this);
        stateJetpack = new StatePlayerJetpack(this);
        stateDie = new StatePlayerDie(this);
        stateAttack = new StatePlayerAttack(this);

        this.AddTransition(new Transition(stateIdle, stateWalk));
        this.AddTransition(new Transition(stateIdle, stateRun));
        this.AddTransition(new Transition(stateIdle, stateFalling));
        this.AddTransition(new Transition(stateIdle, stateJump));
        this.AddTransition(new Transition(stateIdle, stateLanding));
        this.AddTransition(new Transition(stateIdle, stateLadder));
        this.AddTransition(new Transition(stateIdle, stateJetpack));
        this.AddTransition(new Transition(stateIdle, stateDie));
        this.AddTransition(new Transition(stateIdle, stateAttack));

        this.AddTransition(new Transition(stateWalk, stateIdle));
        this.AddTransition(new Transition(stateWalk, stateRun));
        this.AddTransition(new Transition(stateWalk, stateFalling));
        this.AddTransition(new Transition(stateWalk, stateJump));
        this.AddTransition(new Transition(stateWalk, stateLanding));
        this.AddTransition(new Transition(stateWalk, stateLadder));
        this.AddTransition(new Transition(stateWalk, stateJetpack));
        this.AddTransition(new Transition(stateWalk, stateDie));
        this.AddTransition(new Transition(stateWalk, stateAttack));


        this.AddTransition(new Transition(stateRun, stateIdle));
        this.AddTransition(new Transition(stateRun, stateWalk));
        this.AddTransition(new Transition(stateRun, stateFalling));
        this.AddTransition(new Transition(stateRun, stateJump));
        this.AddTransition(new Transition(stateRun, stateLanding));
        this.AddTransition(new Transition(stateRun, stateLadder));
        this.AddTransition(new Transition(stateRun, stateJetpack));
        this.AddTransition(new Transition(stateRun, stateDie));
        this.AddTransition(new Transition(stateRun, stateAttack));


        this.AddTransition(new Transition(stateFalling, stateIdle));
        this.AddTransition(new Transition(stateFalling, stateWalk));
        this.AddTransition(new Transition(stateFalling, stateRun));
        this.AddTransition(new Transition(stateFalling, stateJump));
        this.AddTransition(new Transition(stateFalling, stateLanding));
        this.AddTransition(new Transition(stateFalling, stateLadder));
        this.AddTransition(new Transition(stateFalling, stateJetpack));
        this.AddTransition(new Transition(stateFalling, stateDie));
        this.AddTransition(new Transition(stateFalling, stateAttack));

        this.AddTransition(new Transition(stateJump, stateIdle));
        this.AddTransition(new Transition(stateJump, stateWalk));
        this.AddTransition(new Transition(stateJump, stateRun));
        this.AddTransition(new Transition(stateJump, stateFalling));
        this.AddTransition(new Transition(stateJump, stateLanding));
        this.AddTransition(new Transition(stateJump, stateLadder));
        this.AddTransition(new Transition(stateJump, stateJetpack));
        this.AddTransition(new Transition(stateJump, stateDie));
        this.AddTransition(new Transition(stateJump, stateAttack));

        this.AddTransition(new Transition(stateLanding, stateIdle));
        this.AddTransition(new Transition(stateLanding, stateWalk));
        this.AddTransition(new Transition(stateLanding, stateRun));
        this.AddTransition(new Transition(stateLanding, stateJump));
        this.AddTransition(new Transition(stateLanding, stateFalling));
        this.AddTransition(new Transition(stateLanding, stateLadder));
        this.AddTransition(new Transition(stateLanding, stateJetpack));
        this.AddTransition(new Transition(stateLanding, stateDie));
        this.AddTransition(new Transition(stateLanding, stateAttack));

        this.AddTransition(new Transition(stateLadder, stateIdle));
        this.AddTransition(new Transition(stateLadder, stateWalk));
        this.AddTransition(new Transition(stateLadder, stateRun));
        this.AddTransition(new Transition(stateLadder, stateJump));
        this.AddTransition(new Transition(stateLadder, stateFalling));
        this.AddTransition(new Transition(stateLadder, stateLanding));
        this.AddTransition(new Transition(stateLadder, stateJetpack));
        this.AddTransition(new Transition(stateLadder, stateDie));
        this.AddTransition(new Transition(stateLadder, stateAttack));

        this.AddTransition(new Transition(stateJetpack, stateIdle));
        this.AddTransition(new Transition(stateJetpack, stateWalk));
        this.AddTransition(new Transition(stateJetpack, stateRun));
        this.AddTransition(new Transition(stateJetpack, stateJump));
        this.AddTransition(new Transition(stateJetpack, stateFalling));
        this.AddTransition(new Transition(stateJetpack, stateLanding));
        this.AddTransition(new Transition(stateJetpack, stateLadder));
        this.AddTransition(new Transition(stateJetpack, stateDie));
        this.AddTransition(new Transition(stateJetpack, stateAttack));

        this.AddTransition(new Transition(stateDie, stateIdle));
        this.AddTransition(new Transition(stateDie, stateWalk));
        this.AddTransition(new Transition(stateDie, stateRun));
        this.AddTransition(new Transition(stateDie, stateJump));
        this.AddTransition(new Transition(stateDie, stateFalling));
        this.AddTransition(new Transition(stateDie, stateLanding));
        this.AddTransition(new Transition(stateDie, stateLadder));
        this.AddTransition(new Transition(stateDie, stateJetpack));
        this.AddTransition(new Transition(stateDie, stateAttack));

        this.AddTransition(new Transition(stateAttack, stateIdle));
        this.AddTransition(new Transition(stateAttack, stateWalk));
        this.AddTransition(new Transition(stateAttack, stateRun));
        this.AddTransition(new Transition(stateAttack, stateJump));
        this.AddTransition(new Transition(stateAttack, stateFalling));
        this.AddTransition(new Transition(stateAttack, stateLanding));
        this.AddTransition(new Transition(stateAttack, stateLadder));
        this.AddTransition(new Transition(stateAttack, stateJetpack));
        this.AddTransition(new Transition(stateAttack, stateDie));
    }
}
