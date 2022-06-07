using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouEnemyController : MonoBehaviour
{
    //����
    public AudioClip audioClipFix;
    public AudioClip audioClipHit;
    private AudioSource audioSource;
    //�ٶ�
    public float speed;
    //ȷ���Ƿ�x,y���ƶ�
    public bool IsVertical = false;
    Rigidbody2D rigidbody2;
    //�ƶ�ʱ��
    public float changeTime = 3.0f;
    //��ʱ��
    float timer;
    //����
    int direction = 1;
    //����
    Animator animator;
    //�趨һ��ֵ���������˺û�
    bool broken = true;
    //��ȡ���Ӷ���
    public ParticleSystem SmokeEffict;
    void Start()
    {
        timer = changeTime;
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
    }
    void Update()
    {
        //����������޺��ˣ������ƶ�
        if(!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if(timer<0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void FixedUpdate()
    {
        //����������޺��ˣ������ƶ�
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2.position;
        if(IsVertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
        }
        rigidbody2.MovePosition(position);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();
        if(rubyController!=null)
        {
            rubyController.ChangeHealth(-1);
        }
    }
    public void Fix()
    {
       //����״̬��ȡ����ײЧ��
       broken = false;
        //ȡ����������
        rigidbody2.simulated = false;
        //�����޸�����
        animator.SetTrigger("Fix");
        //����������Ч
        SmokeEffict.Stop();
        //������Ƶ
        audioSource.PlayOneShot(audioClipFix);
        audioSource.PlayOneShot(audioClipHit);
        //ֹͣ��·����
        GetComponent<AudioSource>().Stop();
    }
}
