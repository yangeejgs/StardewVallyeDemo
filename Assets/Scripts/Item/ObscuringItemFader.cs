
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// (1)���������SpriteRenderer���
/// (2)Awake�г�ʼ��SpriteRenderer���
/// (3)���嵭�롢����������Э�̷�����
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ObscuringItemFader : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRontine());
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInRontine());
    }

    /// <summary>
    /// Э�̵��뷽��
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInRontine()
    {
        // ��ȡ��ǰ���������Alphaֵ
        float currentAlpha = spriteRenderer.color.a;
        // ���㵭��Ŀ��Ĳ�ֵ
        float distance = 1 - currentAlpha;
        // ѭ�����õ���Ч��
        while (1 - currentAlpha > 0.01f)
        {
            currentAlpha += distance / Settings.fadeInSeconds * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        // �������յ���ֵ
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    /// <summary>
    /// Э�̵�������
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutRontine()
    {
        // ��ȡ��ǰ���������Alphaֵ
        float currentAlpha = spriteRenderer.color.a;
        // ���㵭��Ŀ��Ĳ�ֵ
        float distance = currentAlpha - Settings.targetAlpha;
        // ѭ�����õ���Ч��
        while (currentAlpha - Settings.targetAlpha > 0.01f)
        {
            currentAlpha -= distance / Settings.fadeOutSeconds * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        // �������յ���ֵ
        spriteRenderer.color = new Color(1f, 1f, 1f, Settings.targetAlpha);
    }
}
