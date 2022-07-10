using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 5.0f;
    public float dectivate_Timer = 12f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactivate", dectivate_Timer);
    }

    // Update is called once per frame
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
        gameObject.SetActive(false);
    }
}
