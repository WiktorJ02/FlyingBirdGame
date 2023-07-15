using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    Rigidbody2D rb2d;
    Animator anim;
    public LogicScript logic;
    public Text scoreText;
    public CapsuleCollider2D capsuleCollider;
    private int score = 0;
    private bool pointsAdded = false;
    public bool isBirdAlive = true;
    public AudioSource quack;
    public AudioSource gameOver;
    bool contact = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isBirdAlive == true)
        {
            rb2d.velocity = Vector2.up * velocity;
            anim.SetTrigger("Jump");
            PlayQuack();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") && !pointsAdded)
        {
            score++;
            UpdateScoreText();
            pointsAdded = true;
            Invoke("ResetPointsAdded", 2f);
        }
        if (collision.CompareTag("Obstacle") && !contact)
        {
            contact = true;
            PlayGameOver();
            logic.gameOver();
            isBirdAlive = false;
            Invoke("ColliderOff", 0.3f);
        }
        if (collision.CompareTag("End"))
        {
            logic.winGame();
            isBirdAlive = false;
        }
    }
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void ResetPointsAdded()
    {
        pointsAdded = false;
    }

    void ColliderOff()
    {
        capsuleCollider.enabled = false;
    }
    public void PlayQuack()
    {
        quack.Play();
    }
    public void PlayGameOver()
    {
        gameOver.Play();
    }
}

