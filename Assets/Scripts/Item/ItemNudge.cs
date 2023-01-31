using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    private WaitForSeconds pause;

    private bool isAnimating = false;

    private void Awake()
    {
        pause = new WaitForSeconds(0.04f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAnimating)
        {
            // 当触发碰撞时，物体在玩家左边，逆时针旋转，否则顺时针旋转
            if (gameObject.transform.position.x < collision.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isAnimating)
        {
            if (gameObject.transform.position.x > collision.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    private IEnumerator RotateAntiClock()
    {
        // 将动画状态改为true
        isAnimating = true;
        // 逆时针旋转4次，每次2f
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        // 顺时针旋转5次，每次2f
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        // 逆时针旋转1次，每次2f
        gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
        yield return pause;
        // 将动画状态改为false
        isAnimating = false;
    }

    private IEnumerator RotateClock()
    {
        isAnimating = true;
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
        yield return pause;
        isAnimating = false;
    }

}
