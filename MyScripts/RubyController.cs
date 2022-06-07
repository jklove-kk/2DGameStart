using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController: MonoBehaviour
{
    public float speed = 0.1f;
    private Rigidbody2D rigidbody2D;
    public int maxHealth = 5;
    //应用属性，保护封装性
    public int health 
    { get { return currentHealth; }  }
    int currentHealth;
    float horizontal;
    float vertical;
    //受伤无敌时间
    public float timeInvincible = 2.0f;
    //是否无敌
    bool isInvincible;
    //无敌时间计时器
    float invincibleTime;
    //创建动画对象
    Animator animatorRuby;
    //设置ruby静止和移动的时候的动画参数
    Vector2 LookDirection = new Vector2(1, 0);
    Vector2 move;
    //设置一个公开的游戏对象用来挂接子弹预制件
    public GameObject projectilePrefab;
    //设置子弹飞行速度(即力的大小)
    public float force;
    //声明声源对象
    private AudioSource audioSource;
    //子弹实例化的音效
    public AudioClip AudioClipLaunch;
    //受伤音效
    public AudioClip audioClipHit;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animatorRuby = GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //判断无敌状态，更改计时器
        if(isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime<=0)
            {
                isInvincible = false;
            }
        }
        //获取移动参数
        move = new Vector2(horizontal, vertical);
        //判断是否在移动
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            LookDirection.Set(move.x, move.y);
            LookDirection.Normalize();
        }
        //传递Ruby面朝方向
        animatorRuby.SetFloat("Look X", LookDirection.x);
        animatorRuby.SetFloat("Look Y", LookDirection.y);
        //magnitue返回矢量长度
        animatorRuby.SetFloat("Speed", move.magnitude);

        //子弹发射逻辑
        if(Input.GetKeyDown(KeyCode.J))
        {
            Launch();
        }
        //投射射线激活NPC对话
        if(Input.GetKeyDown(KeyCode.X))
        {
            //记录投射碰撞的对象
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, LookDirection,1.5f,LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                Debug.Log($"投射对象是{hit.collider.gameObject}");
                //调用npc对话框
                NonPlayerCharacters npc = hit.collider.GetComponent<NonPlayerCharacters>();
                if (npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if(amount<0)
        {
            //无敌状态判断方法
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTime = timeInvincible;
            //受伤动画
            animatorRuby.SetTrigger("Hit");
            //受伤音效
            audioSource.PlayOneShot(audioClipHit);
        }
        currentHealth = Mathf.Clamp(amount + currentHealth, 0, maxHealth);
        //更改生命ui
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        Debug.Log($"生命值为:{currentHealth}" + "/" + maxHealth);
    }
    //发射子弹
    void Launch()
    {
        //创建子弹游戏对象
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        //发射音效
        audioSource.PlayOneShot(AudioClipLaunch);
        //获取子弹脚本组件
        projectile projectile = projectileObject.GetComponent<projectile>();
        //调用子弹发射
        projectile.Launch(LookDirection, force);
        //子弹发射动画
        animatorRuby.SetTrigger("Launch");
    }
    //播放指定音频的方法
    public void PlaySound(AudioClip audioClip)
    {
        //调用音频源的PlayOneShot(audioCliP)方法播放音频
        audioSource.PlayOneShot(audioClip);
    }
    public void PlaySound(AudioClip audioClip,float SoundValue)
    {
        //调用音频源的PlayOneShot(audioCliP,SoundValue)方法播放音频
        audioSource.PlayOneShot(audioClip,SoundValue);
    }

}
