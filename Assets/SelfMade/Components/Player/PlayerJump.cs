using UnityEngine;

public class PlayerJump : MonoBehaviour
{
  public bool IsOnGround { get; private set; }
  private PlayerInputController playerInputController;
  public Animator animator;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private float groundCheckRadius = 0.15f;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private float jumpForce = 3f;
  private Rigidbody2D rb;

  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    rb = GetComponent<Rigidbody2D>();

  }
  private void HandleJump()
  {
    if (!IsOnGround) return;
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    animator.SetTrigger("Jump");

  }
  private void Update()
  {
    IsOnGround = Physics2D.OverlapCircle(
        groundCheck.position,
        groundCheckRadius,
        groundLayer
    );
  }
  void OnEnable()
  {
    playerInputController.OnJump += HandleJump;
  }
  void OnDisable()
  {
    playerInputController.OnJump -= HandleJump;
  }
  private void OGizmosSelected()
  {
    if (groundCheck == null)
    {
      return;
    }

    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
  }

}