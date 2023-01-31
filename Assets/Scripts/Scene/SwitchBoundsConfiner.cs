
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
    /// �л����������������Ļ��Ե����ײ��
    /// </summary>
    private void SwitchBoundsShape()
    {
        // ��ȡ��BoundsConfiner tag�Ķ���Ķ������ײ��
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        // ��ȡ�Լ��������ȷ�������
        CinemachineConfiner2D cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;
        // ��ȷ���߽߱緢���仯��ʱ���������
        cinemachineConfiner.InvalidateCache();
    }
}
