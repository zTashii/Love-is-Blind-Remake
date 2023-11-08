using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    public GameObject playerLight;

    public GameObject worldSpawn;
    public int numberOfLives = 3;

    public LayerMask groundLayer;

    public List<Image> flashIcon = new List<Image>();
    public Sprite flashFull;
    public Sprite flashEmpty;
    public bool used;

    //Potential Variables
    [SerializeField] private bool isGrounded = true;
    private bool timerTriggered = false;
    public bool flashing;
    private Rigidbody2D myRigidBody;
    private HealthManager healthManager;
    public GameObject currentSafeSpot;
    private static PlayerController _instance;
    public bool isFacingRight = true;

    private void Start()
    {
        this.canMove = true;
        used = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        
        flashCount = 3;
    }
    private void Awake()
    {
        if (PlayerController._instance == null)
        {
            PlayerController._instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else if (this != PlayerController._instance)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            return;
        }
        healthManager = GetComponent<HealthManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene sc, LoadSceneMode mode)
    {
        ResetFlashes();
        currentSafeSpot = null;
        ResetLife();
       
    }


    public void ResetFlashes()
    {
        flashCount = 3;
    }
    public void ResetLife()
    {
        healthManager.Heal();
    }

    private void Update()
    {
        Flash();
        ResetScene();
        GroundCheck();
        Jump();
        InitialiseFlashIcons();
        if (!worldSpawn)
        {
            worldSpawn = GameObject.Find("World Spawn");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void InitialiseFlashIcons()
    {
        for(int i = 0; i < flashIcon.Count; i++)
        {
            if( i < flashCount)
            {
                flashIcon[i].sprite = flashFull;
            }
            else
            {
                flashIcon[i].sprite = flashEmpty;
            }
            
        }
    }

    public void Move()
    {
        if (this.canMove)
        {
            myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidBody.velocity.y);
            if (isFacingRight && Input.GetAxisRaw("Horizontal") > 0f)
            {
                Flip();
            }
            else if (!isFacingRight && Input.GetAxisRaw("Horizontal") < 0f)
            {
                Flip();
            }
        }
        else
        {
            myRigidBody.velocity = Vector2.zero;
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = this.transform.localScale;
        localScale.x *= -1f;
        this.transform.localScale = localScale;


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


    public void TeleportToSafeSpot()
    {
        this.gameObject.transform.position = currentSafeSpot.transform.position;
    }

    public void TeleportToWorldSpawn()
    {
        this.gameObject.transform.position = worldSpawn.transform.position;
    }

    public void ResetScene()
    {
        if (Input.GetKeyDown(KeyCode.R)&& SceneManager.GetActiveScene().name != "Tutorial")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            Debug.Log("You have reached your goal");
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.CompareTag("FlashPickup"))
        {
            if(flashCount < 3)
            { 
                flashCount++;
                Destroy(collision.gameObject);
            }
            
        }
        if(collision.CompareTag("Safe Spot"))
        {
            currentSafeSpot = collision.gameObject;
        }
        if(collision.CompareTag("Death Barrier"))
        {
            healthManager.TakeDamage();
        }
    }
}
