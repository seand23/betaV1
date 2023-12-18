using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;

    private Camera mainCam;
    private Vector3 pointToLook;

    public bool hasPowerup;
    public GameObject powerupIndicator;

    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject rocketPrefab;
    public float boostSpeed;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    public ParticleSystem bloodParticle;
    public ParticleSystem bloodParticle2;

    private GameManager gameManager;

    private Animator playerAnimator;


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        //setting playerRB to the RB component
        playerRb = GetComponent<Rigidbody>();

        //setting mainCam to object type camera (theres only 1 so it has to be main cam)
        mainCam = FindObjectOfType<Camera>();

        //setting game manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        
    }
    void Update()
    {
        Debug.Log("current speed = " + speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, 2, 0);

        if (gameManager.isGameActive)
        {
            //player movement using WASD or arrow keys
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            playerRb.AddForce(Vector3.forward * speed * verticalInput);
            playerRb.AddForce(Vector3.right * speed * horizontalInput);


            //controlling the camera with the mouse
            //setting ray to mouse position
            Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
            //creating plane
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            //creating a float to store ray length for debug to see a line
            float rayLength;

            //if we hit the plane 
            if (groundPlane.Raycast(camRay, out rayLength))
            {
                //creat a vector to look from camera
                pointToLook = camRay.GetPoint(rayLength);
                //to see a line (check if its working)
                Debug.DrawLine(camRay.origin, pointToLook, Color.red);
                //make the player look at the mouse position
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            //powerup managment player side
            if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
            {
                LaunchRockets();
            }
            if (currentPowerUp == PowerUpType.Health)
            {
                gameManager.UpdateHealth(-1);
            }
            if (currentPowerUp == PowerUpType.Speed)
            {
                speed = boostSpeed;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power up"))
        {
            powerupIndicator.gameObject.SetActive(true);
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            hasPowerup = true;
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdown());
        }
    }
    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7);
        speed = 5;
        powerupIndicator.gameObject.SetActive(false);
        currentPowerUp = PowerUpType.None;
        hasPowerup = false;
    }

    //rocket powerup
    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    //tracking health loss if collision with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("FastZombie") || collision.gameObject.CompareTag("MiniZombie"))
        {
            gameManager.UpdateHealth(1);
            Debug.Log("hit");
            // making blood come out of player
            bloodParticle.Play();
            bloodParticle2.Play();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            gameManager.UpdateHealth(2);
            // making blood come out of player
            bloodParticle.Play();
            bloodParticle2.Play();
        }

    }
}
