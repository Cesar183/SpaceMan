using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 6f;
    public float runningSpeeed = 2f;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 startPosition;
    private const string STATE_ALIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";
    [SerializeField]
    private int healthPoints, manaPoints;
    public const int INITIAL_HEALTH = 50, INITIAL_MANA = 15,
        MAX_HEALTH = 100, MAX_MANA = 30, 
        MIN_HEALTH = 10, MIN_MANA = 0;
    public LayerMask groundMask;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        startPosition = this.transform.position;
    }
    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        Invoke("ReStartPosition", 0.2f);
    }
    void ReStartPosition()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }
    void Update()
    {
        if(Input.GetButtonDown("Jump")) //(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
        float angle = 45f;
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad));
        Debug.DrawRay(this.transform.position, direction * 1.8f, Color.red);
    }
    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if(rigidBody.velocity.x < runningSpeeed)
            {
                rigidBody.velocity = new Vector2(runningSpeeed, rigidBody.velocity.y);
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }
    void Jump()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if(IsTouchingTheGround())
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
    bool IsTouchingTheGround()
    {
        float angle = 45f;
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad));
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundMask) || 
           Physics2D.Raycast(this.transform.position, direction, 1.8f, groundMask) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Die()
    {
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if(travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }
    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if(this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
        if(this.healthPoints <= 0)
        {
            Die();
        }
    }
    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if(this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }
    public int GetHealth()
    {
        return healthPoints;
    }
    public int GetMana()
    {
        return manaPoints;
    }
    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
}
