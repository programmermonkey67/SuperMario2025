using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 startPosition;

    public float movementSpeed = 5f;
    public float jumpForce = 10;
    public float bounceForce = 4;
    public int direction = 1;
    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider;
    public Vector3 initialPosition;
    public Vector3 finalPosition;

    private InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;
    public AudioClip deathSFX;
    public AudioClip jumpSFX;
    public Rigidbody2D rBody2D;
    private SpriteRenderer render;
    private GroundSensor sensor;
    private Animator animator;
    private BGMManager _bgmManagerScript;
    public AudioClip win;
}
    void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        sensor = GetComponentInChildren<GroundSensor>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        transform.position = startPosition;  
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();

        if(moveDirection.x > 0)
        {
            render.flipX = false;
            animator.SetBool("IsRunning", true);
        }
        else if(moveDirection.x < 0)
        {
            render.flipX = true;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        

        if(jumpAction.WasPressedThisFrame() && sensor.isGrounded)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(jumpSFX);
        }

        animator.SetBool("IsJumping", !sensor.isGrounded);
    }

    void FixedUpdate()
    {
        rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
    }

    private void Bounce()
    {
        rBody2D.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
       } 

    public void Mariodeath()
    {
        _bgmManagerScript.StopBGM();
        
        animator.SetBool("IsDeath", true);

        _audioSource.PlayOneShot(deathSFX);

        movementSpeed = 0;

        _boxCollider.enabled = false;

        Destroy(gameObject, 2f); 
    
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Win")
        {
            _bgmManagerScript.Win();
            _audioSource.PlayOneShot(win);
}   }