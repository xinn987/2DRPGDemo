using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    public Transform groundCheck;
    public float groundCheckDistance;
    public Transform wallCheck;
    public float wallCheckDistance;
    public LayerMask groundLayerMask;
    
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }

    #region Collision Detection

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, facingDir * Vector2.right, wallCheckDistance, groundLayerMask);
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
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
        rb.velocity = new Vector2(xVelocity, yVelocity);
        if (checkFlip)
            FlipController(xVelocity);
    }

    /**
     * 设置速度为0
     */
    public void SetVelocityZero() => rb.velocity = new Vector2(0, 0);

    #endregion
    
}
