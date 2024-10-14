using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ThrowSkill : Skill
{
    [Header("Throw Skill")]
    [SerializeField] private GameObject throwPrefab;
    [SerializeField] private float throwSpeed;
    [SerializeField] private float throwGravityScale;
    private Vector2 throwDirection;

    [Header("AimIndicator")] 
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int dotsNum;
    [SerializeField] private float dotsInterval;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dotsArr;

    protected override void Start()
    {
        base.Start();
        GenerateAimIndicatorDots();
    }

    protected override void Update()
    {
        base.Update();
        // 按住按键时，持续更新每个点的位置
        if (Input.GetKey(KeyCode.Mouse2))
        {
            Vector2 throwDirection = GetThrowDirection();
            for (int i = 0; i < dotsArr.Length; i++)
            {
                dotsArr[i].transform.position = CalcAimIndicatorDotPosition(throwDirection, i * dotsInterval);
            }
            // 玩家面向瞄准方向
            if (throwDirection.x * player.facingDir < 0)
            {
                player.Flip();
            }
        }
        // 松开按键，固化throw方向
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            throwDirection = GetThrowDirection();
        }
    }

    public void Create(Transform spawnPoint)
    {
        GameObject throwObject = Instantiate(throwPrefab);
        throwObject.GetComponent<ThrowController>().Setup(spawnPoint, throwDirection * throwSpeed, throwGravityScale);
    }

    /**
     * 根据当前鼠标位置，计算扔出点到鼠标的方向
     */
    public Vector2 GetThrowDirection()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPoint = player.transform.position;
        
        Vector2 direction = mousePoint - playerPoint;
        return direction.normalized;
    }
    
    public void SetAimIndicatorActivate(bool isActive)
    {
        for (int i = 0; i < dotsNum; i++)
        {
            dotsArr[i].SetActive(isActive);
        }
    }

    /**
     * 初始化瞄准指示器中的点对象数组，初始置为不可见
     */
    private void GenerateAimIndicatorDots()
    {
        dotsArr = new GameObject[dotsNum];
        for (int i = 0; i < dotsNum; i++)
        {
            GameObject dot = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dot.SetActive(false);
            dotsArr[i] = dot;
        }
    }

    /**
     * 根据瞄准方向，通过自由落体公式生成抛物线点的位置
     */
    private Vector2 CalcAimIndicatorDotPosition(Vector2 currentDirection, float t)
    {
        Vector2 positionOffset = throwSpeed * t * currentDirection + 0.5f * throwGravityScale * t * t * Physics2D.gravity;
        return (Vector2) player.transform.position + positionOffset;
    }
    
}
