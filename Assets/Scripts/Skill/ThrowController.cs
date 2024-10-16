using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D cd;
    private Player player;

    private bool isReturning;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowObjectReturn();
        }
        
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 
                player.skillManager.throwSkill.returnSpeed * Time.deltaTime);
            transform.right = (transform.position - player.transform.position).normalized;
            if (Vector2.Distance(transform.position, player.transform.position) < 0.5)
            {
                player.skillManager.throwSkill.ClearThrowInstance();
            }
        }
    }

    public void Setup(Transform _spawnPoint, Vector2 _throwForce, float _throwGravityScale)
    {
        transform.position = _spawnPoint.position;
        rb.velocity = _throwForce;
        rb.gravityScale = _throwGravityScale;
        
        animator.SetBool("Spinning", true);
    }

    private void ThrowObjectReturn()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        transform.parent = null;
        isReturning = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.right = rb.velocity;
        
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = other.transform;
        
        animator.SetBool("Spinning", false);
    }
}
