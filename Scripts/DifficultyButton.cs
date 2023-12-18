using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        gameManager.StartGame();
        Debug.Log(gameObject.name + "was clicked");
        if (gameObject.name == "Easy Button") 
        {
            SpawnManager.waveCount = 0;
        }
        else if (gameObject.name == "Medium Button")
        {
            SpawnManager.waveCount = 4;
        }
        else if (gameObject.name == "Hard Button")
        {
            SpawnManager.waveCount = 9;
        }
    }
}
