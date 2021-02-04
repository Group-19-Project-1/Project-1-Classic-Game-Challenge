using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacdotBehavior : MonoBehaviour
{
    public GameObject scoreText;
    public int theScore;
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "Player")
            theScore += 100;
            scoreText.GetComponent<Text>().text = "SCORE: " + theScore;
            Destroy(gameObject);
    }
}
