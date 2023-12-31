using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField]
    private float _speed;

    int angle;
    int maxAngle = 20;
    int minAngle = -60;

    public Score score;
    bool touchedGround;

    public GameManager gameManager;
    public Sprite fishDied;
    SpriteRenderer sp;
    Animator anim;
    public ObstackleSpawner obstackleSpawner;
    

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        //_rb.gravityScale = -1;
        sp = GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
        
    }

    
    void Update()
    {
        FishSwim();
                        

    }

    private void FixedUpdate()
    {
        FishRotation();
    }


    void FishSwim()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.gameOver == false)
        {
            
            if (GameManager.gameStarted == false)
            {
                _rb.gravityScale = 2f;
                _rb.velocity = Vector2.zero;
                _rb.velocity = new Vector2(_rb.velocity.x, _speed);
                obstackleSpawner.InstantiateObstackle();
                gameManager.GameHasStarted();
            }
            else
            {
                _rb.velocity = Vector2.zero;
                _rb.velocity = new Vector2(_rb.velocity.x, _speed);
            }

            
        }
    }

    void FishRotation()
    {
        if (_rb.velocity.y > 0)
        {
            if (angle <= maxAngle)
            {
                angle = angle + 4;
            }
        }
        else if (_rb.velocity.y < -1.2)
        {
            if (angle > minAngle)
            {
                angle = angle - 2;
            }
        }

        if (touchedGround == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstackle"))
        {
            score.Scored();
        }
        else if (collision.CompareTag("Column"))
        {
            //gameOver
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player Died!");

            if (GameManager.gameOver == false)
            {
                //gameOver
                gameManager.GameOver();
                GameOver();
            }
            else
            {
                //gameOverfish
                GameOver();

            }
        }
    }

    void GameOver()
    {
        touchedGround = true;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        sp.sprite = fishDied;
        anim.enabled= false;
    }

}
