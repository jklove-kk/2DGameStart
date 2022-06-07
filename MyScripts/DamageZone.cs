using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //…˘“Ù
    public AudioClip audioClip;
    public float SoundValue;
    //…À—™¡ø
    public int damageNum = -1;
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(damageNum);
            //≤•∑≈“Ù∆µ
            rubyController.PlaySound(audioClip, SoundValue);
        }
    }
}
