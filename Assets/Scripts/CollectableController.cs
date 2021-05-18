using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public int force;
    public Sprite[] sprites;

    Rigidbody2D rb2d;
    BoxCollider2D boxCollider2d;

    float screenTopY;
    float screenBottomY;

    // Start is called before the first frame update
    void Start()
    {
        screenTopY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
        screenBottomY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;

        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        int randomIndex = Random.Range (0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[randomIndex];

        StartCoroutine ((IEnumerator)SpawnCollectable());
    }

    void Update()
    {
        if (transform.position.y > screenTopY)
        {
            transform.position = new Vector2(transform.position.x, screenBottomY);
        }
    }

    IEnumerable SpawnCollectable()
    {
        boxCollider2d.isTrigger = false;
        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);

        boxCollider2d.isTrigger = true;

        yield return new WaitForSeconds(0.5f);

        rb2d.gravityScale = 0;
        rb2d.velocity = new Vector2(0, 0);
    }
}
