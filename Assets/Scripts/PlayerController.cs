using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int jumpForce;
    public int playerSpeed;
    public float shootDelay;

    public bool isGrounded;
    public float groundRadius;
    public LayerMask whatIsGround;
    public Transform[] groundPoints;

    public GameObject bubblePrefab;
    public Transform bubbleSpawnPoint;

    bool canShoot;
    Vector2 direction;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;

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
        if (Input.GetKeyDown(KeyCode.Space) && canShoot == true)
        {
            StartCoroutine (ShootBubble());
        }
    }

    IEnumerator ShootBubble ()
    {
        GameObject bubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
        bubble.GetComponent<BubbleController>().direction = direction;

        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy")){
            KillPlayer();

        }
    }

    void KillPlayer()
    {
        Destroy(this.gameObject);
    }
}
