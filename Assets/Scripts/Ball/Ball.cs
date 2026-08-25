using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool ignoreNextCollision;

    public float jumpSpeed;

    public GameObject splashParticle;
    public GameObject splashBounce;
    public float changeSplashY;

    public delegate void WhateverType();
    public WhateverType onCollide;

    private int perfectPass = 0;
    public int perfectPassToDestroy = 3;
    public bool isSuperSpeedActive;

    private Rigidbody rb;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource bounceSoundEffect;
    [SerializeField]
    private AudioSource deadSoundEffect;
    [SerializeField]
    private AudioSource scoreSoundEffect;

    [Header("Animations")]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        EffectsOnSuperSpeed();
        if (perfectPass >= 4)
        {
            rb.linearDamping = 0.9f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            bounceSoundEffect.Play();

            collision.transform.parent.GetComponent<PassCheck>().DestroyPlatforms();

            Jump();

            Instantiate(splashBounce, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            
            //Handheld.Vibrate();
        }
        else if (collision.gameObject.CompareTag("Platform") && !GameManager.singleton.isGameOver)
        {           

            bounceSoundEffect.Play();

            Jump();

            GameObject Splash = Instantiate(splashParticle, new Vector3(transform.position.x, transform.position.y - changeSplashY, transform.position.z), GetRandomRotation());
            Splash.transform.parent = collision.gameObject.transform;
            Destroy(Splash, 5);
            Instantiate(splashBounce, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            //Handheld.Vibrate();
        }
        else if (collision.gameObject.CompareTag("Dead") && !isSuperSpeedActive && !GameManager.singleton.isGameOver)
        {
            deadSoundEffect.Play();
            GameManager.singleton.GameOver();


            rb.GetComponent<SphereCollider>().enabled = false;
            rb.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);

            animator.SetTrigger("DeadBall");


            //Handheld.Vibrate();            
        }

        perfectPass = 0;
        rb.linearDamping = 0;
        scoreSoundEffect.pitch = 1;
        isSuperSpeedActive = false;
        if (onCollide != null) onCollide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PassCheck"))
        {
            perfectPass++;
            scoreSoundEffect.pitch = scoreSoundEffect.pitch + perfectPass * 0.1f;
            if (perfectPass >= perfectPassToDestroy)
            {
                isSuperSpeedActive = true;
            }

            scoreSoundEffect.Play();
        }
    }

    void EffectsOnSuperSpeed()
    {
        if (isSuperSpeedActive)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    private void Jump()
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        animator.SetTrigger("BounceBall");
    }
    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(90, Random.Range(0, 360), 0); ;
    }
}
