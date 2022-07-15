using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombScript : MonoBehaviour
{

    private GameObject _target;
    private Vector2 _fixedTargetToFollow;
    private int _movespeed = 10;
    private Rigidbody2D _rigibody;


    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _fixedTargetToFollow = _target.transform.position;
        Invoke("DestroyProjectile", 5f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(_rigibody.position, _fixedTargetToFollow, _movespeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //DestroyProjectile();
            this.gameObject.SetActive(false);
        }
    }

    void DestroyProjectile()
    {
        gameObject.GetComponentInParent<BombEnemyController>().ProjectilePool.Release(this.gameObject);

    }
}
