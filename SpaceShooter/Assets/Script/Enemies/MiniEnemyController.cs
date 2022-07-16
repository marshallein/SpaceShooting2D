using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyController : MonoBehaviour, ICloneable
{
    public BasicEnemy enemyInfo;
    public static event EventHandler MiniEnemyDestroyed;
    private int _health;
    [SerializeField]
    private int _moveSpeed;
    private Rigidbody2D _rigidbody;
    private EnemySpawner _spawner;

    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
        _health = enemyInfo.health;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Deactivate());
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            MiniEnemyDestroyed?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        _health = enemyInfo.health;
    }

    void Move()
    {
        _rigidbody.velocity = new Vector2(Vector2.left.x * _moveSpeed, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            _health--;
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(7);
        _spawner.MiniPool.Release(this);
    }

    public object Clone()
    {
        return this.MemberwiseClone() as MiniEnemyController;
    }
}
