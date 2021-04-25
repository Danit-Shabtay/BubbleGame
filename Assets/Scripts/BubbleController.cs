using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float speed;
    public float shootSpeed;
    public Vector2 direction;
    public float timeBetweenScale;

    Rigidbody2D rb2d;
    bool isSpawnFinish;
    float originalSpeed;
    Vector2 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
        originalScale = transform.localScale;

        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(SpawnBubble());

        StartCoroutine(AnimateBubble());
    }

    IEnumerator AnimateBubble()
    {
        Vector2 smallScale = originalScale / 1.05f;

        while (true)
        {
            if (isSpawnFinish == true)
            {
                transform.localScale = smallScale;
                yield return new WaitForSeconds(0.2f);

                transform.localScale = originalScale;
                yield return new WaitForSeconds(0.2f);
            } else
            {
                yield return null;
            }
        }
    }

    IEnumerator SpawnBubble()
    {
        isSpawnFinish = false;

        speed = shootSpeed;

        for (int i = 3; i > 0; i--)
        {
            transform.localScale = originalScale / i;

            yield return new WaitForSeconds(timeBetweenScale);

            speed = speed / 2;
        }
        isSpawnFinish = true;
        speed = originalSpeed;
        direction = Vector2.up;
    }

    void FixedUpdate()
    {
        rb2d.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChangeDirection"))
        {
            direction = other.GetComponent<ChangeDirection>().direction;
        }
    }
}
