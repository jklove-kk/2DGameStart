using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController: MonoBehaviour
{
    public float speed = 0.1f;
    private Rigidbody2D rigidbody2D;
    public int maxHealth = 5;
    //Ӧ�����ԣ�������װ��
    public int health 
    { get { return currentHealth; }  }
    int currentHealth;
    float horizontal;
    float vertical;
    //�����޵�ʱ��
    public float timeInvincible = 2.0f;
    //�Ƿ��޵�
    bool isInvincible;
    //�޵�ʱ���ʱ��
    float invincibleTime;
    //������������
    Animator animatorRuby;
    //����ruby��ֹ���ƶ���ʱ��Ķ�������
    Vector2 LookDirection = new Vector2(1, 0);
    Vector2 move;
    //����һ����������Ϸ���������ҽ��ӵ�Ԥ�Ƽ�
    public GameObject projectilePrefab;
    //�����ӵ������ٶ�(�����Ĵ�С)
    public float force;
    //������Դ����
    private AudioSource audioSource;
    //�ӵ�ʵ��������Ч
    public AudioClip AudioClipLaunch;
    //������Ч
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
        //�ж��޵�״̬�����ļ�ʱ��
        if(isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime<=0)
            {
                isInvincible = false;
            }
        }
        //��ȡ�ƶ�����
        move = new Vector2(horizontal, vertical);
        //�ж��Ƿ����ƶ�
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            LookDirection.Set(move.x, move.y);
            LookDirection.Normalize();
        }
        //����Ruby�泯����
        animatorRuby.SetFloat("Look X", LookDirection.x);
        animatorRuby.SetFloat("Look Y", LookDirection.y);
        //magnitue����ʸ������
        animatorRuby.SetFloat("Speed", move.magnitude);

        //�ӵ������߼�
        if(Input.GetKeyDown(KeyCode.J))
        {
            Launch();
        }
        //Ͷ�����߼���NPC�Ի�
        if(Input.GetKeyDown(KeyCode.X))
        {
            //��¼Ͷ����ײ�Ķ���
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, LookDirection,1.5f,LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                Debug.Log($"Ͷ�������{hit.collider.gameObject}");
                //����npc�Ի���
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
            //�޵�״̬�жϷ���
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTime = timeInvincible;
            //���˶���
            animatorRuby.SetTrigger("Hit");
            //������Ч
            audioSource.PlayOneShot(audioClipHit);
        }
        currentHealth = Mathf.Clamp(amount + currentHealth, 0, maxHealth);
        //��������ui
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        Debug.Log($"����ֵΪ:{currentHealth}" + "/" + maxHealth);
    }
    //�����ӵ�
    void Launch()
    {
        //�����ӵ���Ϸ����
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        //������Ч
        audioSource.PlayOneShot(AudioClipLaunch);
        //��ȡ�ӵ��ű����
        projectile projectile = projectileObject.GetComponent<projectile>();
        //�����ӵ�����
        projectile.Launch(LookDirection, force);
        //�ӵ����䶯��
        animatorRuby.SetTrigger("Launch");
    }
    //����ָ����Ƶ�ķ���
    public void PlaySound(AudioClip audioClip)
    {
        //������ƵԴ��PlayOneShot(audioCliP)����������Ƶ
        audioSource.PlayOneShot(audioClip);
    }
    public void PlaySound(AudioClip audioClip,float SoundValue)
    {
        //������ƵԴ��PlayOneShot(audioCliP,SoundValue)����������Ƶ
        audioSource.PlayOneShot(audioClip,SoundValue);
    }

}
