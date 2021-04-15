using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerSpeed;
    Vector2 direction;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (inputX < 0)
        {
            direction = Vector2.left;
        }
        else if (inputX > 0)
        {
            direction = Vector2.right;
        }

        transform.localScale = new Vector2(direction.x, transform.localScale.y);

        rb2d.velocity = new Vector2(inputX * playerSpeed, rb2d.velocity.y);
    }
}
