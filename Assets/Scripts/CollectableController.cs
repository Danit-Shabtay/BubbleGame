using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableController : MonoBehaviour
{
    public int force;

    Rigidbody2D rb2d;
    BoxCollider2D boxCollider2d;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        int randomIndex = Random.Range (0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[randomIndex];

        StartCoroutine ((string)SpawnCollectable());
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
