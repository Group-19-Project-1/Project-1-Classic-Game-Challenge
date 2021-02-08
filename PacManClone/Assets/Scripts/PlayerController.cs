using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Text winText;
    public Text GameOverText;
    bool gameOver = false;
    bool isActive = false;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public float speed = 3.0f;

    public int maxHealth = 1;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public Text countText;
    public int count;

    Rigidbody2D rigidbody2d;

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        count = 0;
        SetCountText();

        winText.text = "";
        GameOverText.text = "";

        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (Input.GetKeyDown(KeyCode.R))
        {
            if(gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if(gameOver == true || count == 36)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOver == true || count == 36)
            {
                Application.Quit();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(count == 64)
            {
                SceneManager.LoadScene("SecondLevel");
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if(other.gameObject.CompareTag("Power Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            PowerUps();

        }


    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        if(currentHealth == 0)
        {
            GameOverText.text = "Game Over. R to restart. M for Menu. ESC to quit.";
            speed = 0;
            gameOver = true;

        }//This "hides" the player while leaving script active.

    }

    void SetCountText ()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        countText.text = "Count: " + count.ToString();
        if (sceneName == "FirstLevel")
        {
            if (count == 64)
            {
                winText.text = "Press X to move to the next level.";
            }

        }

        else if (sceneName == "SecondLevel")
        {
            if (count == 36)
            {
                winText.text = "You've done it Boy! You got all your candy back! Press Escape to end the Game!";
            }

        }
    }

   void PowerUps()
    {
        if ( isActive == false)
        {

            isActive = true;
            StartCoroutine(PowerUpTimer(5f));// starts time function


        }



    }

    IEnumerator PowerUpTimer(float waitTime)
    {
        speed = speed + 5.0f; // adds speed
        yield return new WaitForSeconds(waitTime);
        speed = speed - 5.0f; // removes speed
        isActive = false;
    }





}
