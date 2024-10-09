using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBacckground : MonoBehaviour
{
    public float parallaxSpeed;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private float spriteWidth;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        CalcParallax();
        EndlessMove();
    }

    /**
     * 滚动视差
     */
    private void CalcParallax()
    {
        // 获取摄像机本帧的移动距离，乘以视差系数，作为背景的移动距离，系数越小移动越慢
        var deltaCameraPosition = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaCameraPosition.x * parallaxSpeed, 0, 0);
        lastCameraPosition = cameraTransform.position;
    }

    /**
     * 滚动平移
     */
    private void EndlessMove()
    {
        // 当摄像机和背景的位置差大于背景的半个宽度即为超出边界，此时移动背景整个宽度的距离
        if (cameraTransform.position.x - transform.position.x > spriteWidth / 2)
        {
            transform.position += new Vector3(spriteWidth, 0, 0);
        }
        else if (cameraTransform.position.x - transform.position.x < -spriteWidth / 2)
        {
            transform.position += new Vector3(-spriteWidth, 0, 0);
        }
    }
}