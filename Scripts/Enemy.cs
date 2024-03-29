using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeeed = 1.5f;
    public int enemyDamage = 20;
    Rigidbody2D rigidBody;
    public bool facingRight = false;
    private Vector3 startPosition;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }
    void Start()
    {
        //this.transform.position = startPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeeed;
        if(facingRight)
        {
            currentRunningSpeed = runningSpeeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentRunningSpeed = -runningSpeeed;
            this.transform.eulerAngles = Vector3.zero;
        }
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            return;
        }
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }
        facingRight = !facingRight;
    }
}
