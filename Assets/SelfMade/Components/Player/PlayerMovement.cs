using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerInputController inputController;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private float climbSpeed = 4f;

  public bool IsOnLadder { get; private set; }
  public bool IsClimbing { get; private set; }

  private float defaultGravity;
  private Rigidbody2D rb;
  public float moveSpeed = 5f;
  public Animator animator;
  public PlatformMoving currentPlatform { get; private set; }
  private PlayerLiveState playerLiveState;


  void Awake()
  {
    inputController = GetComponent<PlayerInputController>();
    rb = GetComponent<Rigidbody2D>();
    playerLiveState = GetComponent<PlayerLiveState>();
    defaultGravity = rb.gravityScale;
  }


  private void Update()
  {
    float horizontalInput = inputController.MoveInput.x;
    if (LevelComponent.Instance.isCompleted)
    {
      horizontalInput = 0f;

    }

    if (playerLiveState.IsDead) return;
    animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

    if (horizontalInput > 0.01f)
    {
      spriteRenderer.flipX = false;
    }
    else if (horizontalInput < -0.01f)
    {
      spriteRenderer.flipX = true;
    }



  }
  private void FixedUpdate()
  {
    if (LevelComponent.Instance.isCompleted) return;
    if (playerLiveState.IsDead) return;

    Vector2 input = inputController.MoveInput;

    // Подъём начинается, когда игрок находится на лестнице
    // и нажимает вверх или вниз.
    if (IsOnLadder && Mathf.Abs(input.y) > 0.01f)
    {
      IsClimbing = true;
    }

    if (IsClimbing)
    {
      rb.gravityScale = 0f;

      rb.linearVelocity = new Vector2(
          input.x * moveSpeed,
          input.y * climbSpeed
      );

      return;
    }

    rb.gravityScale = defaultGravity;

    float platformVelocityX = 0f;

    if (currentPlatform != null)
    {
      platformVelocityX = currentPlatform.Velocity.x;
    }

    rb.linearVelocity = new Vector2(
        input.x * moveSpeed + platformVelocityX,
        rb.linearVelocity.y
    );
  }

  public void SetCurrentPlatform(PlatformMoving platform)
  {
    currentPlatform = platform;
  }

  public void ClearCurrentPlatform(PlatformMoving platform)
  {
    if (currentPlatform == platform)
    {
      currentPlatform = null;
    }
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Ladder"))
    {
      IsOnLadder = true;
      Debug.Log("ere");
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Ladder"))
    {
      IsOnLadder = false;
      IsClimbing = false;
      rb.gravityScale = defaultGravity;
    }
  }

}