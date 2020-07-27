using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] GameManager gameManager;
    [SerializeField] SpriteRenderer getReady;
    [SerializeField] float flapForce;

    [SerializeField] AudioClip flapSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip pointSound;
    [SerializeField] AudioClip swoosh;

    [SerializeField] GameObject deathScreen;

    bool deathScreenShown = false;
    bool deathTriggeredTwice = false;

    AudioSource flapSource;
    AudioSource deathSource;
    AudioSource hitSource;
    AudioSource pointSource;
    AudioSource swooshSource;

    // Start is called before the first frame update
    void Start()
    {
        flapSource = gameManager.AddAudio(gameObject, flapSound, false, false, 1);
        deathSource = gameManager.AddAudio(gameObject, deathSound, false, false, 1);
        hitSource = gameManager.AddAudio(gameObject, hitSound, false, false, 1);
        pointSource = gameManager.AddAudio(gameObject, pointSound, false, false, 1);
        swooshSource = gameManager.AddAudio(gameObject, swoosh, false, false, 1);
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !animator.GetBool("Dead"))
        {
            if (!animator.GetBool("GameStarted"))
            {
                StartGame();
            }
            Flap();
        }
        else if (Input.GetMouseButtonDown(0) && deathScreenShown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (animator.GetBool("Flap"))
            animator.SetBool("Flap", false);
    }

    void StartGame()
    {
        animator.SetBool("GameStarted", true);
        gameManager.gameStarted = true;
        getReady.enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void Flap()
    {
        flapSource.Play();
        animator.SetBool("Flap", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, flapForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("Dead") == false)
        {
            hitSource.Play();
            animator.SetBool("Dead", true);
            animator.SetBool("GameStarted", false);
            gameManager.gameStarted = false;

            if (collision.gameObject.name != "Ground")
            {
                animator.SetBool("FallDeath", true);
                deathSource.Play();
            }
        }
        else if(!deathTriggeredTwice)
        {
            StartCoroutine(ShowDeathScreen());
        }
    }

    IEnumerator ShowDeathScreen()
    {
        deathTriggeredTwice = true;
        yield return new WaitForSeconds(1);
        deathScreenShown = true;
        swooshSource.Play();
        deathScreen.GetComponent<Animator>().SetBool("Down", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pointSource.Play();
    }
}
