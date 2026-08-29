using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("jump tuning")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 1f;
    [SerializeField] private const int MaxJumps = 2;
    [SerializeField] private int JumpsLeft = MaxJumps;


    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool IsGrounded;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerControls.Movement.jump.performed += OnJump;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    { 
        PlayerInput();

        CheckIsgrounded();

        HandleJumpVariable();

        UpdateAnimations();

        FlipSprite();
    }

    private void FixedUpdate()
    {
        Move();

        HandleBetterFall();
    }

    private void PlayerInput()
    {
        // gets type vector2 from player action map
        movement = playerControls.Movement.move.ReadValue<Vector2>();
    }

    private void CheckIsgrounded() 
    {
        // if circle overlaps with a hitbox on ground layer, IsGrounded = true
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
     
    }

    private void Move()
    {
        //adds movement speed to the x axis and keeps the y axis the same
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
        
    }

    private void OnJump(InputAction.CallbackContext context) 
    {
        //saves the linear velocity x from walking and adds jumpForce to the linear velocity y
        if (IsGrounded == true)
        {
            JumpsLeft = MaxJumps;   
        }

        if (JumpsLeft > 0 && playerControls.Movement.jump.IsPressed()) 
        {
            if (!IsGrounded) 
            {
                //plays the animation if the player double jumps
                playerAnimator.SetTrigger("DoubleJump");
            }

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            JumpsLeft--;
        }
    }

    private void HandleBetterFall() 
    {
        //if rb y speed is less than zero(aka falling)
        if (rb.linearVelocity.y < 0)
        {
            //add fallmultiplier to player velocity every fixed frame
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    private void HandleJumpVariable()
    {
        //rb is more than zero(accending) AND jump button is not pressed
        bool ReleasedJumpEarly = rb.linearVelocity.y > 0 && !playerControls.Movement.jump.IsPressed();

        if (ReleasedJumpEarly == true) 
        {
            //add lowjumpmultiplier to player velocity every fixed frame
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
           
        }
    }

    private void UpdateAnimations() 
    {
        //sets the moveX float from the animator to the players horizontal movement
        playerAnimator.SetFloat("MoveX", movement.x);

        //sets the bool from the animator to true or false depending on the isGrounded from the script
        playerAnimator.SetBool("IsGrounded", IsGrounded);
    
    }

    private void FlipSprite() 
    {
        //if players x velocity is less than zero(going left), DONT flip sprite
        if (movement.x < 0) 
        {
            spriteRenderer.flipX = false;
        }
        //if players x velocity is more than zero(going right), flip sprite
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

}
