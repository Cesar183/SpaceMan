using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 8f;
    public float runningSpeed = 2f;
    public LayerMask groundMask;
    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }
    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        Invoke("RestartPosition", 0.2f);
    }
    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
    }
    void Update()
    {
        if(Input.GetButtonDown("Jump")) //(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
        //Debug.DrawRay(this.transform.position, Vector2.one * 1.5f, Color.red);
    }
    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, //x
                                                rigidBody.velocity.y); //y
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
            if (IsTouchingTheGround())
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        
    }
    bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position,Vector2.down,1.5f,groundMask))
        {
            //Logica de dontacto con el suelo
            return true;
        }
        else
        {
            return false; 
        }
    }
    public void Die()
    {
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }
}
