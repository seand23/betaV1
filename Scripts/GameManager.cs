using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //managing score
    public TextMeshProUGUI scoreText;
    private int score;
    //managing health
    public TextMeshProUGUI healthText;
    private int maxHealth = 5;
    private int health = 3;
    //restart game
    public Button restartButton;
    //game over
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;

    public GameObject titleScreen;
    public GameObject gameScreen;

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
    }

    // Update score with value from each enemy hit (used in CollisionTest.cs)
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    //update score when the player is hit by an enemy updated through PlayerController.cs
    public void UpdateHealth(int healthLoss)
    {
        health -= healthLoss;
        if (health > maxHealth)
        {
            health = maxHealth;
            Debug.Log("health is greater than the limit");
        }
        if (health == 5)
        {
            healthText.text = "\u1F60 \u1F60 \u1F60 \u1F60 \u1F60";
        }
        else if (health == 4)
        {
            healthText.text = "\u1F60 \u1F60 \u1F60 \u1F60";
        }
        else if (health == 3)
        {
            healthText.text = "\u1F60 \u1F60 \u1F60";
        }
        else if (health == 2)
        {
            healthText.text = "\u1F60 \u1F60";
        }
        else if (health == 1)
        {
            healthText.text = "\u1F60";
        }
        else if(health <= 0)
        {
            GameOver();
            healthText.text = null;

        }
    }

    public void RestartGame()
    {
        //get scene manager class using scene managment library then reloading the current scene being used
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
