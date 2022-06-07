using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonPlayerCharacters : MonoBehaviour
{
    //������ʾʱ��
    public float dispalyTime = 4.0f;
    //��ȡ�ı������
    public GameObject dialogBox;
    //��ʾʱ��
    private float timerDisplay;
   //��ȡTMP����
    public GameObject dlgTMP; 
    //��ȡTMP�ؼ�
    TextMeshProUGUI _tmTxBox;
    //���ñ����洢ҳ��
    int _currentPage;
    //��ҳ��
    int _lastPage;

    void Start()
    {
        //��ʼʱ����ʾ�ı���
        dialogBox.SetActive(false);
        //���ü�ʱ��Ϊ������״̬
        timerDisplay = -0.1f;
        //��ȡTMP�ؼ�
        _tmTxBox = dlgTMP.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //��ȡ��ҳ��
        _lastPage = _tmTxBox.textInfo.pageCount;
        //��ʱ
        if(timerDisplay>=0.0f)
        {
            //��ҳ
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
                //����ǰҳ����ʾ
                _tmTxBox.pageToDisplay = _currentPage;
            }
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0.0f)
            {
                //��Ϊ������
                dialogBox.SetActive(false);
            }
        }
    }
    //npc˵���ķ���
    public void DisplayDialog()
    { 
        _currentPage = 1;
        _tmTxBox.pageToDisplay = _currentPage;
        dialogBox.SetActive(true);
        timerDisplay = dispalyTime;
       

    }
}
