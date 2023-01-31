
using UnityEngine;

/// <summary>
/// ����һ����Ҵ��������������е��뵭���������Ϸ���壬�������ǵĵ��뵭������
/// </summary>
public class TriggerObscuingItemFader : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ʱ��ȡ���� ObscuringItemFader ����������б�������ǵĵ�������
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
        // �뿪����ʱ��ȡ���� ObscuringItemFader ����������б�������ǵĵ��뷽��
        ObscuringItemFader[] obscuringItemFaders = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();
        if (obscuringItemFaders != null && obscuringItemFaders.Length > 0)

            foreach (ObscuringItemFader item in obscuringItemFaders)
            {
                item.FadeIn();
            }
    }

}
