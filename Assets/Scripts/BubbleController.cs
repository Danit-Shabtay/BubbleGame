using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float speed;
    public float shootSpeed;
    public Vector2 direction;
    public float timeBetweenScale;

    public Sprite grayBubble;
    public Sprite transparentBubble;

    float screenTopY;
    float ScreenBottomY;

    Rigidbody2D rb2d;
    bool isSpawnFinish;
    float originalSpeed;
    Vector2 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
        originalScale = transform.localScale;

        screenTopY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
        ScreenBottomY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;

        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(SpawnBubble());
        StartCoroutine(AnimateBubble());
        StartCoroutine(DestroyBubbleOverTime());
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

    IEnumerator DestroyBubbleOverTime()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<SpriteRenderer>().sprite = grayBubble;

        yield return new WaitForSeconds(5f);
        GetComponent<SpriteRenderer>().sprite = transparentBubble;

        yield return new WaitForSeconds(5f);
        DestroyBubble();
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
        if (other.CompareTag("Player"))
        {
            DestroyBubble();
        }
    }

    void DestroyBubble()
    {
        Destroy(this.gameObject);
    }
}
