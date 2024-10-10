using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    public EnemySkeletonIdleState idleState { get; private set; }
    public EnemySkeletonMoveState moveState { get; private set; }
    public EnemySkeletonBattleState battleState { get; private set; }
    public EnemySkeletonAttackState attackState { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemySkeletonIdleState(this, stateMachine, "Idle");
        moveState = new EnemySkeletonMoveState(this, stateMachine, "Move");
        battleState = new EnemySkeletonBattleState(this, stateMachine, "Move");
        attackState = new EnemySkeletonAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
