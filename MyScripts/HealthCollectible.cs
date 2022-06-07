using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //声明音频剪辑
    public AudioClip collectedClip;
    //声音大小变量
    public float SoundValue = 1.0f;
    //回血量设置
    public int amount = 1;
    //回血特效对象
    public ParticleSystem CureEffect;
    private void OnTriggerEnter2D(Collider2D other)
    { 
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null)
        {
            if (rubyController.health < rubyController.maxHealth)
            {
                rubyController.ChangeHealth(amount);
                //播放音频
                rubyController.PlaySound(collectedClip, SoundValue);
                //销毁对象
                Destroy(gameObject);
                //特效实例化
                Instantiate(CureEffect,transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("生命值已满");
            }
        }
        else
        {
            Debug.Log("rubyController 并未取得");
        }
    }
}
