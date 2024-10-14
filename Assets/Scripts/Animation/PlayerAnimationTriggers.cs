using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => PlayerManager.instance.player;
    
    public void AnimationTrigger()
    {
        player.AnimatorTrigger();
    }

    public void AttackTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage(0);
            }
        }
    }

    public void ThrowTrigger()
    {
        player.skillManager.throwSkill.Create(player.transform);
    }
}
