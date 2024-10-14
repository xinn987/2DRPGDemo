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
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.skillManager.throwSkill.SetAimIndicatorActivate(false);
    }
}
