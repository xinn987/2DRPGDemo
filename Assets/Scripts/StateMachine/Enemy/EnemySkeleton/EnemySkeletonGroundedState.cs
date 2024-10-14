using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonGroundState : EnemySkeletonState
{
    public EnemySkeletonGroundState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();
        if (enemySkeleton.IsPlayerDetected() || Vector2.Distance(player.position, enemySkeleton.transform.position) < enemySkeleton.battleDetectRadius)
        {
            stateMachine.ChangeState(enemySkeleton.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
