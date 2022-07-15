using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject circle, dot;
    private Rigidbody2D rb;
    private float moveSpeed;
    private Vector2 touchPosition;
    private Vector2 moveDirection;
    public float upper_Y = 4f;
    public float lower_Y = -4f;
    public float upper_X = 11.2f;
    public float lower_X = -11.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circle.SetActive(false);
        dot.SetActive(false);
        moveSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        circle.SetActive(false);
        dot.SetActive(false);
        rb.velocity = Vector2.zero ;
        if (Input.GetMouseButton(0))
        {
            circle.SetActive(true);
            dot.SetActive(true);
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpaceshipMove();
        }
        SetBoundary();
    }

    private void SpaceshipMove()
    {
        dot.transform.position = touchPosition;
        dot.transform.position = new Vector2(
            Mathf.Clamp(dot.transform.position.x, circle.transform.position.x - 0.8f, circle.transform.position.x + 0.8f),
            Mathf.Clamp(dot.transform.position.y, circle.transform.position.y - 0.8f, circle.transform.position.y + 0.8f));
        moveDirection = (dot.transform.position - circle.transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    private void SetBoundary()
    {
        Vector3 temp = transform.position;
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

        transform.position = temp;
    }
}
