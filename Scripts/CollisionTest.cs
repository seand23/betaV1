using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{

    private Animator animator;
    private float animationDuration = 1.5f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("FastZombie") || collision.gameObject.CompareTag("MiniZombie"))
        {
            Destroy(collision.gameObject, animationDuration);
            Destroy(gameObject, animationDuration);
        }
        //keeping track of score
        if (collision.gameObject.CompareTag("FastZombie"))
        {
            gameManager.UpdateScore(20);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.UpdateScore(5);
        }
        if (collision.gameObject.CompareTag("MiniZombie"))
        {
            gameManager.UpdateScore(1);
        }

    }

}
