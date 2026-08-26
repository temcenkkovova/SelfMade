using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerInputController inputController;
  [SerializeField] private SpriteRenderer spriteRenderer;
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
  void FixedUpdate()
  {
    float platformVelocityX = 0f;
    if (LevelComponent.Instance.isCompleted) return;
    if (playerLiveState.IsDead) return;
    float horizontalInput = inputController.MoveInput.x;

    if (currentPlatform != null)
    {
      platformVelocityX = currentPlatform.Velocity.x;

    }
    if (playerLiveState.IsDead)
    {
      platformVelocityX = 0f;

    }


    rb.linearVelocity = new Vector2(horizontalInput * moveSpeed + platformVelocityX, rb.linearVelocity.y);


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

}