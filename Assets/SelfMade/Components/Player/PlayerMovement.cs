using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerInputController inputController;
  private Rigidbody2D rb;
  void Awake()
  {
    inputController = GetComponent<PlayerInputController>();
    rb = GetComponent<Rigidbody2D>();
  }
  void FixedUpdate()
  {
    if (inputController.MoveInput == new Vector2(0f, 0f)) return;
    float test = inputController.MoveInput.normalized.x > 0 ? 1 : -1;
    rb.linearVelocity = new Vector2(test * 3f, rb.linearVelocity.y);
  }



}