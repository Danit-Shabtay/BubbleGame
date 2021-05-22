using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public Text scoreText;
    public Text playerLives;

    public PlayerController ourPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ourPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + ourPlayer.score;
        playerLives.text = "x " + ourPlayer.lives;
    }
}
