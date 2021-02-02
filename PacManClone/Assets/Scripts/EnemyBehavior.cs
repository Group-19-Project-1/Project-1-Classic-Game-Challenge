using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;

    private Vector2 direction = Vector2.up; 

    private float directionChangeTime = 0.0f;
    public float speed = 1.0f;


    // Use this for initialization
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //this is for direction change
        if (!openDirection(direction))
        {
            if (canChangeDirection())
            {
                changeDirection();
            }
            else if (rb2d.velocity.magnitude < speed)
            {
                changeDirectionAtRandom();
            }
        }
        else if (canChangeDirection() && Time.time > directionChangeTime)
        {
            changeDirectionAtRandom();
        }
        else if (rb2d.velocity.magnitude < speed)
        {
            changeDirectionAtRandom();
        }
        
        //enemy movement
        
        if (rb2d.velocity.x == 0)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        if (rb2d.velocity.y == 0)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
        }
        rb2d.velocity = direction * speed;
    }

    private void changeDirectionAtRandom()
    {
        directionChangeTime = Time.time + 1;
        if (Random.Range(0, 2) > 0)
        {
            changeDirection();
        }
    }

    private bool canChangeDirection()
    {
        Vector2 perpRight = Utility.PerpendicularRight(direction);
        bool openRight = openDirection(perpRight);

        Vector2 perpLeft = Utility.PerpendicularLeft(direction);
        bool openLeft = openDirection(perpLeft);

        return openRight || openLeft;
    }

    private void changeDirection()
    {
        Vector2 perpLeft = Utility.PerpendicularLeft(direction);
        bool openLeft = openDirection(perpLeft);

        Vector2 perpRight = Utility.PerpendicularRight(direction);
        bool openRight = openDirection(perpRight);

        directionChangeTime = Time.time + 1;

        if (openRight || openLeft)
        {
            int choice = Random.Range(0, 2);
            if (!openLeft || (choice == 0 && openRight))
            {
                direction = perpRight;
            }
            else
            {
                direction = perpLeft;
            }
        }
        else
        {
            direction = -direction;
        }
    }

    private bool openDirection(Vector2 dir)
    {
        RaycastHit2D[] rch2ds = new RaycastHit2D[10];

        float dist = 1;

        cc2d.Cast(dir, rch2ds, dist, true);

        foreach (RaycastHit2D rch2d in rch2ds)
        {
            if (rch2d && rch2d.collider.gameObject.tag == "Level")
            {
                return false;
            }
        }
        return true;
    }
}
