using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouEnemyController : MonoBehaviour
{
    //声音
    public AudioClip audioClipFix;
    public AudioClip audioClipHit;
    private AudioSource audioSource;
    //速度
    public float speed;
    //确定是否按x,y轴移动
    public bool IsVertical = false;
    Rigidbody2D rigidbody2;
    //移动时间
    public float changeTime = 3.0f;
    //计时器
    float timer;
    //方向
    int direction = 1;
    //动画
    Animator animator;
    //设定一个值表明机器人好坏
    bool broken = true;
    //获取粒子对象
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
        //如果机器人修好了，不在移动
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
        //如果机器人修好了，不再移动
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
       //更改状态，取消碰撞效果
       broken = false;
        //取消物理引擎
        rigidbody2.simulated = false;
        //播放修复动画
        animator.SetTrigger("Fix");
        //结束粒子特效
        SmokeEffict.Stop();
        //播放音频
        audioSource.PlayOneShot(audioClipFix);
        audioSource.PlayOneShot(audioClipHit);
        //停止走路声音
        GetComponent<AudioSource>().Stop();
    }
}
