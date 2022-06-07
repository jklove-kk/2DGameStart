using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float speed = 2.0f;
    //判断来回移动的条件
    private int Timecount = 0;
    public int WidthRange = 100;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.y = position.y + speed * 0.01f;
        rigidbody2d.MovePosition(position);
        Timecount++;
        if(Timecount>=WidthRange)
        {
            speed = -speed;
            Timecount = 0;

        }
    }

}
