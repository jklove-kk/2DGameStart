using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonPlayerCharacters : MonoBehaviour
{
    //设置显示时间
    public float dispalyTime = 4.0f;
    //获取文本框对象
    public GameObject dialogBox;
    //显示时间
    private float timerDisplay;
   //获取TMP对象
    public GameObject dlgTMP; 
    //获取TMP控件
    TextMeshProUGUI _tmTxBox;
    //设置变量存储页数
    int _currentPage;
    //总页数
    int _lastPage;

    void Start()
    {
        //开始时不显示文本框
        dialogBox.SetActive(false);
        //设置计时器为不可用状态
        timerDisplay = -0.1f;
        //获取TMP控件
        _tmTxBox = dlgTMP.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //获取总页数
        _lastPage = _tmTxBox.textInfo.pageCount;
        //计时
        if(timerDisplay>=0.0f)
        {
            //翻页
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(_currentPage<_lastPage)
                {
                    _currentPage++;
                }
                else
                {
                    _currentPage = 1;
                }
                //将当前页数显示
                _tmTxBox.pageToDisplay = _currentPage;
            }
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0.0f)
            {
                //改为不可用
                dialogBox.SetActive(false);
            }
        }
    }
    //npc说话的方法
    public void DisplayDialog()
    { 
        _currentPage = 1;
        _tmTxBox.pageToDisplay = _currentPage;
        dialogBox.SetActive(true);
        timerDisplay = dispalyTime;
       

    }
}
