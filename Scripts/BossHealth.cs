using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private GameManager gameManager;
    public int health;

    private Animator bossAnimator;

    //audio
    public AudioClip hurtSound;
    private AudioSource bossAudio;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bossAnimator = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        bossAnimator.SetBool("Run", true);
    }

    void Update()
    {
      
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bossAnimator.SetBool("AngryReaction", true);
            bossAnimator.SetBool("Run", false);
            health -= 1;
            bossAudio.PlayOneShot(hurtSound, 1.0f);
        }

        if (health <= 0)
        {
            bossAnimator.SetBool("Dies", true);
            bossAnimator.SetBool("Run", false);
            //kill the boss zombie 
            Destroy(collision.gameObject);
            Destroy(gameObject);

            //keeping track of score
            gameManager.UpdateScore(200);
            //congratulation text for killing/survivng boss
        }



        //animation attempt//trying to trigger 'angryreaction' if the bullet hits the boss
 


    }

}
