using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpaceshipStateControl : MonoBehaviour
{
    [SerializeField]
    public GameObject circle, dot;
    [SerializeField]
    private BulletScript bullet;
    [SerializeField]
    private Transform bullet_spawn;
    SpaceshipBaseState currentState;
    public SpaceshipIdleState idleState = new SpaceshipIdleState();
    public SpaceshipMovingState movingState = new SpaceshipMovingState();
    public SpaceshipCollisionState collisionState = new SpaceshipCollisionState();
    public SpaceshipShootingState shootingState = new SpaceshipShootingState();
    private ObjectPool<BulletScript> _bulletPool;

    private
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);

        _bulletPool = new ObjectPool<BulletScript>(() =>
        {
            var _bullet = Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
            _bullet.OnDeactiveFunction = () =>
            {
                Debug.Log("deactive Bullet");
                _bulletPool.Release(_bullet);
            };
            return _bullet;
        }, bullet =>
        {
            bullet.transform.position = bullet_spawn.position;
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet);
        }, true, defaultCapacity: 10, maxSize: 20);


    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(SpaceshipBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void Shoot()
    {
        _bulletPool.Get();
    }
}
