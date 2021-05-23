using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public Text scoreText;
    public Text playerLives;
    public Text endScoreText;

    public GameObject gameOverPanel;

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

        if (ourPlayer.lives <= 0 && ourPlayer != null)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0;
        endScoreText.text = "Your Score: " + ourPlayer.score;
        gameOverPanel.SetActive(true);
        Destroy(ourPlayer.gameObject);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
