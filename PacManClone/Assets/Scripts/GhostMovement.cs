using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float rayDistance;

    [SerializeField] LayerMask rayLayer;
    [SerializeField] Vector2 currentDir;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    int directionIndex = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDir = directions[directionIndex];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, currentDir, rayDistance, rayLayer);
        Vector3 endpoint = currentDir * rayDistance;

        if (hit2D.collider != null)
        {
            // check if wall ahead
            if (hit2D.collider.gameObject.CompareTag("Wall"))
            {
                ChangeDirection();
            }

            if (hit2D.collider.gameObject.CompareTag("Enemy"))
            {
                ChangeDirection();
            }

            if (hit2D.collider.gameObject.CompareTag("Enemy2"))
            {
                ChangeDirection();
            }

            if (hit2D.collider.gameObject.CompareTag("Enemy3"))
            {
                ChangeDirection();
            }

            if (hit2D.collider.gameObject.CompareTag("Enemy4"))
            {
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        // randomly select between -1 and 1;
        directionIndex += Random.Range(0, 2) * 2 - 1;

        // keeps index from exceeding 3
        int clampedIndex = directionIndex % directions.Length;

            if (clampedIndex < 0)
            {
                clampedIndex = directions.Length + clampedIndex;
            }
       
        rb.velocity = Vector2.zero;
        
        currentDir = directions[clampedIndex];
    }

    void FixedUpdate()
    {
        // move in current direction
        rb.AddForce(currentDir * speed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
