using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    public string animBoolName;
    
    protected float stateTimer;
    protected bool triggerCalled;
    
    protected float xInput;
    protected float yInput;
    
    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", player.rb.velocity.y);
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false); 
    }

    public virtual void AnimationTrigger()
    {
        triggerCalled = true;
    }
}