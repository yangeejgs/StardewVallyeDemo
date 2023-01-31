
using UnityEngine;

/// <summary>
/// 定义一个玩家触碰器，检索所有淡入淡出组件的游戏物体，触发他们的淡入淡出方法
/// </summary>
public class TriggerObscuingItemFader : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 触发时获取带有 ObscuringItemFader 组件的物体列表调用他们的淡出方法
        ObscuringItemFader[] obscuringItemFaders = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();
        if (obscuringItemFaders != null && obscuringItemFaders.Length > 0)
        {
            foreach (ObscuringItemFader item in obscuringItemFaders)
            {
                item.FadeOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 离开触发时获取带有 ObscuringItemFader 组件的物体列表调用他们的淡入方法
        ObscuringItemFader[] obscuringItemFaders = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();
        if (obscuringItemFaders != null && obscuringItemFaders.Length > 0)

            foreach (ObscuringItemFader item in obscuringItemFaders)
            {
                item.FadeIn();
            }
    }

}
