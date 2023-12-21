using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Movement : MonoBehaviour
{
    private bool canMove = true;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 7f;

    [SerializeField]
    private Animator animator;

    public int score = 0;
    private AudioSource aSource;
    public ProgressBar sliderController; // Reference to the SliderController

    [SerializeField]
    private AudioClip collectSound;

    [SerializeField]
    private AudioClip waterCollectSound;

    public TextMeshProUGUI scoreText; // Reference to the Text component

    // Start is called before the first frame update
    void Start()
    {
        if (aSource == null)
        {
            aSource = GetComponent<AudioSource>();
        }
        // It's good practice to ensure that the components are not null
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        StartCoroutine(UpdateScoreText());
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    // It's the right place to update physics-related changes
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Collectable") || other.gameObject.CompareTag("WaterCollectable")) && canMove)
        {
            // Play the appropriate sound
            AudioClip clipToPlay = other.gameObject.CompareTag("Collectable") ? collectSound : waterCollectSound;
            aSource.PlayOneShot(clipToPlay);

            // Update the score
            score += other.gameObject.CompareTag("Collectable") ? 1 : -1;
            sliderController.UpdateSliderValue(score); // Uncomment if slider update is needed
            scoreText.text = "Beers: " + (int)score;

            // Destroy the collectable
            Destroy(other.gameObject);

            // Start the drinking coroutine
            StartCoroutine(DrinkCoroutine());
        }
    }

    private IEnumerator DrinkCoroutine()
    {
        Debug.Log("Started Drink Coroutine");
        canMove = false;

        animator.SetBool("Walk", false);
        animator.SetBool("Drink", true);

        yield return new WaitForSeconds(0.8f);

        Debug.Log("Ending Drink Coroutine");
        animator.SetBool("Drink", false);
        canMove = true;
    }

    private int timer = 0;
    private int currentScore = 0;
    private IEnumerator UpdateScoreText()
    {
        yield return new WaitForSeconds(5f); // Wait for 60 seconds

        canMove = false;
        // scoreText.text = "Score: " + score.ToString(); // Update the score text
        while (timer < score)
        {

            timer++;
            if (currentScore < score)
            {
                currentScore++;
                scoreText.text = "Beers: " + currentScore;
            }
            yield return new WaitForSeconds(0.2f);
        }

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            // Input from the keyboard
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Store the input and calculate the proposed new position
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

            // Check if the new position would collide with an obstacle
            if (!IsCollidingWithObstacle(newPosition))
            {
                rb.MovePosition(newPosition);
                animator.SetBool("Walk", movement != Vector2.zero);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private bool IsCollidingWithObstacle(Vector2 newPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(newPosition, 0.1f, LayerMask.GetMask("Obstacles"));
        return hitCollider != null;
    }

}