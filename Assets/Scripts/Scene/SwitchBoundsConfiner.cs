
using Cinemachine;
using UnityEngine;

public class SwitchBoundsConfiner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SwitchBoundsShape();
    }

    /// <summary>
    /// 切换摄像机用来定义屏幕边缘的碰撞器
    /// </summary>
    private void SwitchBoundsShape()
    {
        // 获取有BoundsConfiner tag的对象的多边形碰撞器
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        // 获取自己的摄像机确认者组件
        CinemachineConfiner2D cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;
        // 当确认者边界发生变化的时候清除缓存
        cinemachineConfiner.InvalidateCache();
    }
}
