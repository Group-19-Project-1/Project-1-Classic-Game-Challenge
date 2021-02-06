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
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if(gameOver == true)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameOver == true)
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
            count = count +1;
            SetCountText();
        }

    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        if(currentHealth == 0)
        {
            GameOverText.text = "Game Over. R to restart. M for Menu. Q to quit.";
            speed = 0;
            gameOver = true;

        }//This "hides" the player while leaving script active.

    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 64)
        {
            winText.text = "Press X to move to the next level.";
        }

    }
}
