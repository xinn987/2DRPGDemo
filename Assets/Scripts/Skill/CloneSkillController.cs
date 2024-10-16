using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloneSkillController : MonoBehaviour
{
    public Transform attackCheck;
    public float cloneTimer;
    
    private float cloneFadeSpeed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        cloneTimer -= Time.deltaTime;
        // 持续时间耗尽，开始消失，彻底消失后删除自身
        if (cloneTimer < 0)
        {
            animator.speed = 0;
            spriteRenderer.color = new Color(
                spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 
                spriteRenderer.color.a - cloneFadeSpeed * Time.deltaTime);
            
            if (spriteRenderer.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(Transform _spawnPoint, float _cloneDuration, float _cloneFadeSpeed)
    {
        transform.position = _spawnPoint.position;
        cloneTimer = _cloneDuration;
        cloneFadeSpeed = _cloneFadeSpeed;
        
        spriteRenderer.color = new Color(0.7f, 0.7f, 1f, 1f);
        animator.SetInteger("AttackNum", Random.Range(1, 4));
        faceToClosestEnemy();
    }

    private void faceToClosestEnemy()
    {
        // 遍历一定范围，获取最近的敌人位置
        Transform closest = null;

        Player player = PlayerManager.instance.player;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, player.attackCheckRadius * 3);
        float minDistance = float.MaxValue;
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(hit.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = hit.transform;
                }
            }
        }

        // 范围内没有敌人，方向为player的方向
        float facingDir = player.facingDir;
        if (closest != null)
        {
            facingDir = closest.position.x - transform.position.x;
        }
        if (facingDir < 0)
        {
            transform.Rotate(0, 180, 0);
        }
    }
    
}
