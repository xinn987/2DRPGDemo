using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowAimState : PlayerState
{
    public PlayerThrowAimState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skillManager.throwSkill.SetAimIndicatorActivate(true);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocityZero();
        // 按住按键时，持续更新每个点的位置
        if (Input.GetKey(KeyCode.Mouse2))
        {
            player.skillManager.throwSkill.OnAimPressing();
        }
        // 松开按键，固化throw方向
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            player.skillManager.throwSkill.OnAimReleased();
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.skillManager.throwSkill.SetAimIndicatorActivate(false);
    }
}
