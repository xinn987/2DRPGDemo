using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillAnimationTriggers : MonoBehaviour
{
    private CloneSkillController skillController;

    private void Awake()
    {
        skillController = GetComponentInParent<CloneSkillController>();
    }

    public void AnimationTrigger()
    {
        // 动画播放完毕立即启动消失
        skillController.cloneTimer = -.1f;
    }

    public void AttackTrigger()
    {
        Player player = PlayerManager.instance.player;
        Collider2D[] hits = Physics2D.OverlapCircleAll(skillController.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage(0);
            }
        }
    }
}
