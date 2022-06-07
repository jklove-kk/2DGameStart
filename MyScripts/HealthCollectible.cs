using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //������Ƶ����
    public AudioClip collectedClip;
    //������С����
    public float SoundValue = 1.0f;
    //��Ѫ������
    public int amount = 1;
    //��Ѫ��Ч����
    public ParticleSystem CureEffect;
    private void OnTriggerEnter2D(Collider2D other)
    { 
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null)
        {
            if (rubyController.health < rubyController.maxHealth)
            {
                rubyController.ChangeHealth(amount);
                //������Ƶ
                rubyController.PlaySound(collectedClip, SoundValue);
                //���ٶ���
                Destroy(gameObject);
                //��Чʵ����
                Instantiate(CureEffect,transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("����ֵ����");
            }
        }
        else
        {
            Debug.Log("rubyController ��δȡ��");
        }
    }
}
