using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnemyController : MonoBehaviour, ICloneable
{
    public enum State
    {
        Move,
        Attack,
        RunOut,
    }

    public BasicEnemy enemyInfo;
    public GameObject projectile;

    [SerializeField]
    private int _moveSpeed = 10;
    private Rigidbody2D _rigibody;
    private Transform _threshold;
    private State _state;
    private bool _canAttack;
    private EnemySpawner _spawner;
    public Transform Threshold { set { _threshold = value; } }
    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //demo threshold
        //GameObject enviroment = GameObject.FindGameObjectWithTag("environment");
        //Threshold = enviroment.transform.Find($"Threshold_Lazer_1").transform;
        _state = State.Move;
        _canAttack = true;
        _rigibody = GetComponent<Rigidbody2D>();
        _spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
    }

    private void OnEnable()
    {
        _state = State.Move;
        _canAttack = true;
        Threshold = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.RunOut:
                RunOut();
                break;
            default:
                return;
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
            StartCoroutine(AttackAction());
        }

    }

    void Move()
    {
        if (_state != State.Move)
        {
            return;
        }
        if (_threshold != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _threshold.position, _moveSpeed * Time.deltaTime);
        }
        if (transform.position.Equals(_threshold.position))
        {
            ChangeState(State.Attack);
        }

    }
    private void RunOut()
    {
        GameObject enviroment = GameObject.FindGameObjectWithTag("environment");
        Threshold = enviroment.transform.Find($"Threshold_Lazer_Out").transform;
        transform.position = Vector2.MoveTowards(transform.position, _threshold.position, _moveSpeed * Time.deltaTime);
        if (transform.position.Equals(_threshold.position))
        {
            _spawner.LazerPool.Release(this);
        }
    }

    void ChangeState(State state)
    {
        _state = state;
    }

    IEnumerator AttackAction()
    {
        projectile.SetActive(true);
        yield return new WaitForSeconds(3);
        projectile.SetActive(false);
        yield return new WaitForSeconds(1);
        ChangeState(State.RunOut);

    }

    public object Clone()
    {
        return this.MemberwiseClone() as LazerEnemyController;
    }
}
