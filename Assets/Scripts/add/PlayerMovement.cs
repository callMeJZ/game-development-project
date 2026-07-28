using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    
    [SerializeField]private ParticleSystem dust;


    private float directionX = 0f;
    public bool doubleJump;


    [SerializeField]private LayerMask wallLayer;
    [SerializeField]private LayerMask jumpableGround;
    //sama og að gera variables public til að unity sjái
    [SerializeField]private float movementSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;


    private enum MovementState {idle,running,jumping,climbing,falling}

    [SerializeField] private AudioSource jumpSound;

    [Header("Wall Jumping")]
    public float wallJumpTime = 0.1f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 1.0f;
    private bool isWallSliding = false;
    RaycastHit2D wallCheckHit;
    float jumpTime;
    private Transform parent;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = GetHorizontalInput();
        bool jumpHeld = IsJumpHeld();
        bool jumpPressed = WasJumpPressedThisFrame();
        bool jumpReleased = WasJumpReleasedThisFrame();
        parent = transform.parent;
        //move left or right
  
        body.linearVelocity = new Vector2(directionX * movementSpeed, body.linearVelocity.y);

       if (IsPlayerGrounded() || parent != null || isWallSliding)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        if ((IsPlayerGrounded() && !jumpHeld && (body.linearVelocity.y >= -0.001f && body.linearVelocity.y <= 0.001f)) || (!jumpHeld && parent != null))
        {
            if (doubleJump)
            {
                CreateDust();
            }
            doubleJump = false;
        }
    
        if ((jumpPressed && (coyoteTimeCounter > 0f || doubleJump)) || (isWallSliding && jumpPressed))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0f;
            jumpSound.Play();
            if (!isWallSliding)
            {
                CreateDust();
            }
            doubleJump = !doubleJump;
        }

        //The longer you hold jump button the higher to jump
        if (jumpReleased && body.linearVelocity.y > 0f)
        {   
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f);
        }

        //Wall Jumping
        if (directionX > 0f)
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayer);
            // Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        }
        else 
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            // Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
        }
        
        if (wallCheckHit && !IsPlayerGrounded() && directionX != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Clamp(body.linearVelocity.y, wallSlideSpeed, float.MaxValue));
        }
        UpdateAnimation();

        
    }

    //check if Player touches the terrain
    private bool IsPlayerGrounded()
    {
        // return Physics2D.OverlapCircle(coll.bounds.center, 1.0f, jumpableGround);
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (directionX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (directionX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if (body.linearVelocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (body.linearVelocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
            
        if (isWallSliding)
        {
            state = MovementState.climbing;
        }
        
        anim.SetInteger("state", (int)state);
    }

    private float GetHorizontalInput()
    {
        float keyboardDirection = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            {
                keyboardDirection -= 1f;
            }
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            {
                keyboardDirection += 1f;
            }
        }

        Gamepad gamepad = Gamepad.current;
        if (gamepad != null && Mathf.Abs(gamepad.leftStick.x.ReadValue()) > Mathf.Abs(keyboardDirection))
        {
            return Mathf.Clamp(gamepad.leftStick.x.ReadValue(), -1f, 1f);
        }

        return Mathf.Clamp(keyboardDirection, -1f, 1f);
    }

    private bool IsJumpHeld()
    {
        Keyboard keyboard = Keyboard.current;
        bool keyboardJump = keyboard != null &&
            (keyboard.spaceKey.isPressed || keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed);

        Gamepad gamepad = Gamepad.current;
        return keyboardJump || (gamepad != null && gamepad.buttonSouth.isPressed);
    }

    private bool WasJumpPressedThisFrame()
    {
        Keyboard keyboard = Keyboard.current;
        bool keyboardJump = keyboard != null &&
            (keyboard.spaceKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame);

        Gamepad gamepad = Gamepad.current;
        return keyboardJump || (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame);
    }

    private bool WasJumpReleasedThisFrame()
    {
        Keyboard keyboard = Keyboard.current;
        bool keyboardJump = keyboard != null &&
            (keyboard.spaceKey.wasReleasedThisFrame || keyboard.wKey.wasReleasedThisFrame || keyboard.upArrowKey.wasReleasedThisFrame);

        Gamepad gamepad = Gamepad.current;
        return keyboardJump || (gamepad != null && gamepad.buttonSouth.wasReleasedThisFrame);
    }

    void OnTriggerEnter2D (Collider2D other)
	{ 
		if (other.gameObject.tag == "Finish")
		{
			GameManager.instance.LoadNextLevel();
		}
	}

    private void CreateDust()
    {
        dust.Play();
    }
}
