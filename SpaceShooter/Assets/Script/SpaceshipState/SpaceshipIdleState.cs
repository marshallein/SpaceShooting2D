using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipIdleState : SpaceshipBaseState
{
    public override void EnterState(SpaceshipStateControl spaceship)
    {
        spaceship.circle.SetActive(false);
        spaceship.dot.SetActive(false);
    }

    public override void UpdateState(SpaceshipStateControl spaceship)
    {
        spaceship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Switch to moving state");
            spaceship.SwitchState(spaceship.movingState);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Switch to shooting state");
            spaceship.SwitchState(spaceship.shootingState);
        }
    }
}
