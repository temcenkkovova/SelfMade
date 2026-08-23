using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerInputController inputController;
  [SerializeField] private SpriteRenderer spriteRenderer;
  private Rigidbody2D rb;
  public float moveSpeed = 3f;
  public Animator animator;
  void Awake()
  {
    inputController = GetComponent<PlayerInputController>();
    rb = GetComponent<Rigidbody2D>();

  }
  private void Update()
  {
    float horizontalInput = inputController.MoveInput.x;

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

    float horizontalInput = inputController.MoveInput.x;

    rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
  }



}