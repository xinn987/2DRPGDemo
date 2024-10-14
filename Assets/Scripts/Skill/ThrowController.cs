using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D cd;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        transform.right = rb.velocity;
    }

    public void Setup(Transform _spawnPoint, Vector2 _throwForce, float _throwGravityScale)
    {
        transform.position = _spawnPoint.position;
        rb.velocity = _throwForce;
        rb.gravityScale = _throwGravityScale;
    }
}
