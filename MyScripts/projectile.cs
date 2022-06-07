using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//子弹脚本
public class projectile : MonoBehaviour
{
    //声音
    
    //刚体对象
    Rigidbody2D rigidbody2D;
    //特效对象
    public ParticleSystem HitEffect;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
      //对子弹飞行距离的限制
      if(transform.position.magnitude>20.0f)
        {
            Destroy(gameObject);
        }
    }
    //子弹飞行
    public void Launch(Vector2 direction,float force)
    {
        //物理系统调用 对对象施加力
        rigidbody2D.AddForce(direction * force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //获取碰撞对象的组件（带youEnemy...的对象）
        YouEnemyController youEnemyController = collision.collider.GetComponent<YouEnemyController>();
        if(youEnemyController != null)
        {
            youEnemyController.Fix();
        }
        Debug.Log($"当前碰撞的对象是{collision.gameObject}");
        //销毁对象
        Destroy(gameObject);

        //实例化特效对象
        Instantiate(HitEffect,transform.position, Quaternion.identity);
    }
}
