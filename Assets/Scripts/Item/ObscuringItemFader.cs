
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// (1)定义必须有SpriteRenderer组件
/// (2)Awake中初始化SpriteRenderer组件
/// (3)定义淡入、淡出方法（协程方法）
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
    /// 协程淡入方法
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInRontine()
    {
        // 获取当前物体组件的Alpha值
        float currentAlpha = spriteRenderer.color.a;
        // 计算淡出目标的差值
        float distance = 1 - currentAlpha;
        // 循环设置淡出效果
        while (1 - currentAlpha > 0.01f)
        {
            currentAlpha += distance / Settings.fadeInSeconds * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        // 设置最终淡出值
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    /// <summary>
    /// 协程淡出方法
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutRontine()
    {
        // 获取当前物体组件的Alpha值
        float currentAlpha = spriteRenderer.color.a;
        // 计算淡出目标的差值
        float distance = currentAlpha - Settings.targetAlpha;
        // 循环设置淡出效果
        while (currentAlpha - Settings.targetAlpha > 0.01f)
        {
            currentAlpha -= distance / Settings.fadeOutSeconds * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        // 设置最终淡出值
        spriteRenderer.color = new Color(1f, 1f, 1f, Settings.targetAlpha);
    }
}
