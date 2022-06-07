using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //����
    public AudioClip audioClip;
    public float SoundValue;
    //��Ѫ��
    public int damageNum = -1;
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(damageNum);
            //������Ƶ
            rubyController.PlaySound(audioClip, SoundValue);
        }
    }
}
