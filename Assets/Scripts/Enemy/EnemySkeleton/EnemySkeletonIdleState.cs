using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonIdleState : EnemySkeletonGroundState
{
    public EnemySkeletonIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemySkeleton.idleTime;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemySkeleton.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
