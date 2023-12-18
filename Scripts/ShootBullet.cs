using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    //projectile
    public GameObject bulletPrefab;
    private float coolDown = 0.7f;
    private float lastSpawn = 0.0f;
    public float bulletSpeed;
    private Rigidbody bulletRigidbody;

    private float aliveTimer = 3.0f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bulletRigidbody = bulletPrefab.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //will add 1 second evry time it passes using the time feature 
        lastSpawn += Time.deltaTime;
        // On spacebar press, send bullet , if last spawn is less than cooldown a bullet will not spawn
        if (gameManager.isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && lastSpawn > coolDown)
            {
                //spawn the bullet with force from an invisble object just in front of the player to give the illusion the bullet
                //is being shot from the player itself
                GameObject bulletHolder = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);

                Rigidbody tempRigidbody = bulletHolder.GetComponent<Rigidbody>();
                tempRigidbody.AddForce(transform.forward * bulletSpeed);

                Destroy(bulletHolder, aliveTimer);
                //reset spawn cooldown timer
                lastSpawn = 0.0f;
            }
        }
    }

}
