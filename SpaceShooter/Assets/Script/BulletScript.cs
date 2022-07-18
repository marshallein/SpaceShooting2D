using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 5.0f;
    public float dectivate_Timer = 12f;

    public delegate void OnDeactive();

    public OnDeactive OnDeactiveFunction { get; set; }
    void Start()
    {
        Invoke("Deactivate", dectivate_Timer);
    }

    private void OnEnable()
    {
        Invoke("Deactivate", dectivate_Timer);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    void Deactivate()
    {
        OnDeactiveFunction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Deactivate();
        }
    }
}
