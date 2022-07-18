using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BombEnemyController : MonoBehaviour, ICloneable
{
    public enum State
    {
        Move,
        Attack,
    }

    public static event EventHandler BombEnemyDestroyed;
    public BasicEnemy enemyInfo;
    public GameObject projectilePrefab;

    private int _health;
    [SerializeField]
    private int _moveSpeed;
    private Rigidbody2D _rigidbody;
    private State _state;
    private Transform _threshold;
    private ObjectPool<GameObject> _projectilePool;
    [SerializeField]
    private float _attackTimer;
    private float _currentAttackTimer;
    private bool _canAttack;
    private EnemySpawner _spawner;
    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public ObjectPool<GameObject> ProjectilePool { get => _projectilePool; }

    // Start is called before the first frame update
    void Start()
    {
        _state = State.Move;
        _health = enemyInfo.health;
        _rigidbody = GetComponent<Rigidbody2D>();

        GameObject enviroment = GameObject.FindGameObjectWithTag("environment");
        _threshold = enviroment.transform.Find("Threshold_BombEnemy").transform;

        _spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();

        _projectilePool = new ObjectPool<GameObject>(() =>
        {
            var projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            projectile.transform.parent = this.transform;
            return projectile;
        }, projectile =>
        {
            projectile.gameObject.SetActive(true);
        }, projecitle =>
        {
            projecitle.gameObject.SetActive(false);
        }, projectile =>
        {
            Destroy(projectile);
        }, true, defaultCapacity: 5, maxSize: 10);

        _currentAttackTimer = _attackTimer;
    }

    private void OnEnable()
    {
        _health = enemyInfo.health;
        _state = State.Move;
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            BombEnemyDestroyed?.Invoke(this, EventArgs.Empty);
            _spawner.BombPool.Release(this);
        }

        _attackTimer += Time.deltaTime;
        if (_attackTimer > _currentAttackTimer)
        {
            _canAttack = true;
        }

    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Move:
                {
                    Move();
                }
                break;
            case State.Attack:
                {
                    Attack();
                }
                break;
            default: return;
        }
    }

    void Move()
    {
        if (_state != State.Move)
        {
            return;
        }
        if (gameObject.transform.position.x <= _threshold.position.x)
        {
            ChangeState(State.Attack);
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            _rigidbody.velocity = new Vector2(Vector2.left.x * _moveSpeed, _rigidbody.velocity.y);
        }
    }

    void Attack()
    {
        if (_state != State.Attack)
        {
            return;
        }

        if (_canAttack)
        {
            _canAttack = false;
            _attackTimer = 0;
            var projectile = _projectilePool.Get();
            projectile.transform.position = this.transform.position;
        }

    }

    void ChangeState(State state)
    {
        _state = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            _health--;
        }
    }

    public object Clone()
    {
        return this.MemberwiseClone() as BombEnemyController;
    }
}
