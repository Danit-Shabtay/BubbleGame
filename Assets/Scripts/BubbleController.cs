using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float speed;
    public float shootSpeed;
    public float enemyCaptureSpeed;
    public Vector2 direction;
    public float timeBetweenScale;
    public int floatingBubblesLayerID;

    public Sprite grayBubble;
    public Sprite transparentBubble;
    public Sprite enemyTrapped;
    public GameObject collectablePrefab;

    public AudioClip enemyPopSound;
    public AudioClip bubblePopSound;

    float bubbleYoffset;
    float screenTopY;
    float ScreenBottomY;

    Rigidbody2D rb2d;
    bool isSpawnFinish;
    bool isEnemyCaptured;
    float originalSpeed;
    Vector2 originalScale;
    AudioSource audioSource;

    Coroutine spawnBubbleRoutine;
    Coroutine destroyBubbleOverTimeRoutine;

    // Start is called before the first frame update
    void Start()
    {
        isEnemyCaptured = false;
        originalSpeed = speed;
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();

        bubbleYoffset = GetComponent<SpriteRenderer>().bounds.extents.y;

        screenTopY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
        ScreenBottomY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;

        rb2d = GetComponent<Rigidbody2D>();

        spawnBubbleRoutine = StartCoroutine (SpawnBubble());

        StartCoroutine(SpawnBubble());
        StartCoroutine(AnimateBubble());
        destroyBubbleOverTimeRoutine = StartCoroutine(DestroyBubbleOverTime());
    }

    private void Update()
    {
        if(transform.position.y > screenTopY + bubbleYoffset)
        {
            transform.position = new Vector2(transform.position.x, ScreenBottomY - bubbleYoffset);
        }
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
        gameObject.layer = floatingBubblesLayerID;
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
        if (other.CompareTag("Wall"))
        {
            StopBubbleSpawn();
        }
        if (other.CompareTag("Enemy"))
        {
            StopBubbleSpawn();
            StopCoroutine(destroyBubbleOverTimeRoutine);

            isEnemyCaptured = true;
            speed = enemyCaptureSpeed;
            GetComponent<SpriteRenderer>().sprite = enemyTrapped;
        }
    }

    void StopBubbleSpawn()
    {
        StopCoroutine(spawnBubbleRoutine);
        isSpawnFinish = true;

        speed = originalSpeed;
        direction = Vector2.up;
        transform.localScale = originalScale;
        gameObject.layer = floatingBubblesLayerID;
    }

    void DestroyBubble()
    {
        AudioClip destroySound;

        if(isEnemyCaptured == true)
        {
            Instantiate(collectablePrefab, transform.position, Quaternion.identity);
            destroySound = enemyPopSound;
        } else
        {
            destroySound = bubblePopSound;
        }
        GetComponent<SpriteRenderer>().enabled = false;

        audioSource.PlayOneShot(destroySound);

        Destroy(this.gameObject,destroySound.length);
    }
}
