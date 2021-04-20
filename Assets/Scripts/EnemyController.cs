using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlatformEnd"))
        {
            Flip();
        }
    }

    void Flip()
    {
        direction = -direction;

        Vector2 newScale = transform.localScale;
        newScale.x = newScale.x * -1;

        transform.localScale = newScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);
    }
}
