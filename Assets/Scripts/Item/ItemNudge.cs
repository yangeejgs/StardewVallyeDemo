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
            // ��������ײʱ�������������ߣ���ʱ����ת������˳ʱ����ת
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
        // ������״̬��Ϊtrue
        isAnimating = true;
        // ��ʱ����ת4�Σ�ÿ��2f
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        // ˳ʱ����ת5�Σ�ÿ��2f
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        // ��ʱ����ת1�Σ�ÿ��2f
        gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
        yield return pause;
        // ������״̬��Ϊfalse
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
