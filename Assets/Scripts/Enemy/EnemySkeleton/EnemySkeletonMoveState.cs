using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonMoveState : EnemySkeletonGroundState
{
    public EnemySkeletonMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemySkeleton.SetVelocity(enemySkeleton.moveSpeed * enemySkeleton.facingDir, enemySkeleton.rb.velocity.y);
        if (enemySkeleton.IsWallDetected() || !enemySkeleton.IsGroundDetected())
        {
            enemySkeleton.Flip();
            stateMachine.ChangeState(enemySkeleton.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
