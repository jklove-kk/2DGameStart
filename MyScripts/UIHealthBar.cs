using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //设置属性(采用静态公开成员)
    public static UIHealthBar instance
    {
        get;
        //只允许在该类设置
        private set;
    }
    //设置遮罩对象(图形对象)
    public Image mask;
    //记录遮罩层初始长度
    private float originalSize;
    
    private void Awake()
    {
        //静态实例为当前对象
        instance = this;
    }
    void Start()
    {
        //获取初始长度
        originalSize = mask.rectTransform.rect.width;
    }

    //创建方法更改mask遮罩层长度
    public void SetValue(float value)
    {
        //value表示变化百分比
        //该方法表示水平方向上mask的宽度（originalSize * value）
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

}
