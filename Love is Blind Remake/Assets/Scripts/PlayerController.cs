using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float moveSpeed;
    public float jumpVelocity;
    public bool canMove;

    public float lowJumpMultiplyer = 3f;
    private float jumpCount;
    private float targetTime = 3f;
    [SerializeField] private int flashCount = 3;

    public int numberOfLives = 3;

    public LayerMask groundLayer;

    public Text flashCounter;
    public bool used;

    //Potential Variables
    [SerializeField] private bool isGrounded = true;
    private bool timerTriggered = false;
    public bool flashing;
    private Rigidbody2D myRigidBody;
    private GameObject worldSpawn;

    private void Start()
    {
        this.canMove = true;
        used = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        worldSpawn = GameObject.FindGameObjectWithTag("World Spawn");
        this.transform.position = this.worldSpawn.transform.position;
        flashCount = 3;
    }

    private void Update()
    {
        Flash();
        GroundCheck();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        if (this.canMove)
        {
            myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidBody.velocity.y);
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                transform.localScale = new Vector2(1, 1);

            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    public void Jump()
    {
        if (this.canMove)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidBody.velocity = Vector2.up * jumpVelocity;
            }
            if (jumpCount != 1 && Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidBody.velocity = Vector2.up * jumpVelocity;
                jumpCount = 1;
            }
            if (myRigidBody.velocity.y < 0)
            {
                myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;
            }
            else if (myRigidBody.velocity.y > 0 && !Input.GetButtonDown("Jump"))
            {
                myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplyer - 1) * Time.deltaTime;
            }
        }
    }

    public void GroundCheck()
    { 
        float distance = 1f;
        Vector2 position = this.transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        Debug.DrawRay(this.transform.position, direction * distance);
        if (hit.collider != null)
        {
            isGrounded = true;
            jumpCount = 0;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Flash()
    {

        if (Input.GetKeyDown(KeyCode.F) && flashCount != 0)
        {
            this.canMove = false;
            if (!flashing)
            {
                flashing = true;
                timerTriggered = true;
                flashCount -= 1;
                flashCounter.text = "\n" + flashCount.ToString();
            }
        }
        if (timerTriggered)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                this.canMove = true;
                flashing = false;
                targetTime = 3;
                timerTriggered = false;
            }
        }

    }


    public void TeleportToWorldSpawn()
    {
        gameObject.transform.position = worldSpawn.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Goal"))
        {
            Debug.Log("You have reached your goal");
        }

    }
}
