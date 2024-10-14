using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonState : EnemyState
{
    protected EnemySkeleton enemySkeleton;
    
    public EnemySkeletonState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemySkeleton = (EnemySkeleton)enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
