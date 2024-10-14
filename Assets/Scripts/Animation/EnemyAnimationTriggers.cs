using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();
    
    public void AnimationTrigger()
    {
        enemy.AnimatorTrigger();
    }
    
    public void AttackTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage(0);
            }
        }
    }

    public void OpenCounterStunWindow() => enemy.OpenCounterStunWindow();
    
    public void CloseCounterStunWindow() => enemy.CloseCounterStunWindow();
}
