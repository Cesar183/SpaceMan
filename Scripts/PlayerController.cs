using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 6f;
    Rigidbody2D rigidBody;
    public LayerMask groundMask;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }
    void Jump()
    {
        if(IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position,
                             Vector2.down,
                             2f,
                             groundMask))
        {
            //Logica de dontacto con el suelo
            return true;
        }
        else
        {
            return false; 
        }
    }
}
