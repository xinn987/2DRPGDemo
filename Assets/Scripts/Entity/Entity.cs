using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    public Transform groundCheck;
    public float groundCheckDistance;
    public Transform wallCheck;
    public float wallCheckDistance;
    public LayerMask groundLayerMask;
    public Transform attackCheck;
    public float attackCheckRadius;
    
    [Header("Knock Back Info")]
    public float knockBackDuration;
    public Vector2 knockBackDirection;
    private bool isKnocked = false;
    
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX entityFX { get; private set; }
    #endregion

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entityFX = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage(float damage)
    {
        entityFX.StartCoroutine(nameof(EntityFX.FlashFX));
        StartCoroutine(nameof(KnockBack));
        Debug.Log(gameObject.name + " damaged");
    }

    public IEnumerator KnockBack()
    {
        SetVelocity(knockBackDirection.x * -facingDir, knockBackDirection.y, false);
        isKnocked = true;
        yield return new WaitForSeconds(knockBackDuration);
        isKnocked = false;
    }

    #region Collision Detection

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, facingDir * Vector2.right, wallCheckDistance, groundLayerMask);
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion
    
    #region Flip Functions
    
    public void FlipController(float x)
    {
        if (x > 0 && !facingRight || x < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    #endregion

    #region Velocity Functions

    /**
     * 设置速度
     * 可以设置是否进行翻转判断，用于避免物理碰撞导致的少量速度回弹
     */
    public void SetVelocity(float xVelocity, float yVelocity, bool checkFlip = true)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        if (checkFlip)
            FlipController(xVelocity);
    }

    /**
     * 设置速度为0
     */
    public void SetVelocityZero()
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(0, 0);
    }

    #endregion
    
}
