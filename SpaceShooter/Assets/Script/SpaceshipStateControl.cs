using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipStateControl : MonoBehaviour
{
    [SerializeField]
    public GameObject circle, dot;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bullet_spawn;
    SpaceshipBaseState currentState;
    public SpaceshipIdleState idleState = new SpaceshipIdleState();
    public SpaceshipMovingState movingState = new SpaceshipMovingState();
    public SpaceshipCollisionState collisionState = new SpaceshipCollisionState();
    public SpaceshipShootingState shootingState = new SpaceshipShootingState();
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
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
        Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
    }
}
