using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player;

    [SerializeField] protected float cooldown;
    private float cooldownTimer;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        return cooldownTimer < 0;
    }

    public virtual void UseSkill()
    {
        ResetCooldownTimer();
    }

    private void ResetCooldownTimer()
    {
        cooldownTimer = cooldown;
    }
}
