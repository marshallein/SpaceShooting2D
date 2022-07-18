using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovingState : SpaceshipBaseState
{
    private float moveSpeed = 5f;
    private Vector2 touchPosition;
    private Vector2 moveDirection;
    public float upper_Y = 4f;
    public float lower_Y = -4f;
    public float upper_X = 11.2f;
    public float lower_X = -11.2f;
    public override void EnterState(SpaceshipStateControl spaceship)
    {
        spaceship.circle.SetActive(true);
        spaceship.dot.SetActive(true);
    }

    public override void UpdateState(SpaceshipStateControl spaceship)
    {
        if (Input.GetMouseButton(0))
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpaceshipMove(spaceship);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            spaceship.SwitchState(spaceship.shootingState);
        }
        else
        {
            spaceship.SwitchState(spaceship.idleState);
        }
    }

    private void SpaceshipMove(SpaceshipStateControl spaceship)
    {
        spaceship.dot.transform.position = touchPosition;
        spaceship.dot.transform.position = new Vector2(
            Mathf.Clamp(spaceship.dot.transform.position.x, spaceship.circle.transform.position.x - 0.8f, spaceship.circle.transform.position.x + 0.8f),
            Mathf.Clamp(spaceship.dot.transform.position.y, spaceship.circle.transform.position.y - 0.8f, spaceship.circle.transform.position.y + 0.8f));
        moveDirection = (spaceship.dot.transform.position - spaceship.circle.transform.position).normalized;
        spaceship.GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
        SetBoundary(spaceship);
    }

    private void SetBoundary(SpaceshipStateControl spaceship)
    {
        Vector3 temp = spaceship.transform.position;
        if (temp.y > upper_Y)
        {
            temp.y = upper_Y;
        }
        else if (temp.y < lower_Y)
        {
            temp.y = lower_Y;
        }
        else if (temp.x > upper_X)
        {
            temp.x = upper_X;
        }
        else if (temp.x < lower_X)
        {
            temp.x = lower_X;
        }
        spaceship.transform.position = temp;
    }
}
