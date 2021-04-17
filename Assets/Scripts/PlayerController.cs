using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int jumpForce;
    public int playerSpeed;

    Vector2 direction;
    Rigidbody2D rb2d;

    public bool isGrounded;
    public float groundRadius;
    public LayerMask whatIsGround;
    public Transform[] groundPoints;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = checkIsGrounded();

        if(isGrounded && Input.GetKeyDown("up"))
        {
            rb2d.AddForce(new Vector2 (0, jumpForce), ForceMode2D.Impulse);
        }
    }

    bool checkIsGrounded()
    {
        for (int i = 0; i < groundPoints.Length; i++)
        {
            if (Physics2D.OverlapCircle(groundPoints[i].position, groundRadius, whatIsGround))
            {
                return true;
            }    
        }
        return false;
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
