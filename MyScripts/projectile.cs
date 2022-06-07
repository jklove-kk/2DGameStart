using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ӵ��ű�
public class projectile : MonoBehaviour
{
    //����
    
    //�������
    Rigidbody2D rigidbody2D;
    //��Ч����
    public ParticleSystem HitEffect;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
      //���ӵ����о��������
      if(transform.position.magnitude>20.0f)
        {
            Destroy(gameObject);
        }
    }
    //�ӵ�����
    public void Launch(Vector2 direction,float force)
    {
        //����ϵͳ���� �Զ���ʩ����
        rigidbody2D.AddForce(direction * force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //��ȡ��ײ������������youEnemy...�Ķ���
        YouEnemyController youEnemyController = collision.collider.GetComponent<YouEnemyController>();
        if(youEnemyController != null)
        {
            youEnemyController.Fix();
        }
        Debug.Log($"��ǰ��ײ�Ķ�����{collision.gameObject}");
        //���ٶ���
        Destroy(gameObject);

        //ʵ������Ч����
        Instantiate(HitEffect,transform.position, Quaternion.identity);
    }
}
