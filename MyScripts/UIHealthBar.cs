using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //��������(���þ�̬������Ա)
    public static UIHealthBar instance
    {
        get;
        //ֻ�����ڸ�������
        private set;
    }
    //�������ֶ���(ͼ�ζ���)
    public Image mask;
    //��¼���ֲ��ʼ����
    private float originalSize;
    
    private void Awake()
    {
        //��̬ʵ��Ϊ��ǰ����
        instance = this;
    }
    void Start()
    {
        //��ȡ��ʼ����
        originalSize = mask.rectTransform.rect.width;
    }

    //������������mask���ֲ㳤��
    public void SetValue(float value)
    {
        //value��ʾ�仯�ٷֱ�
        //�÷�����ʾˮƽ������mask�Ŀ�ȣ�originalSize * value��
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

}
