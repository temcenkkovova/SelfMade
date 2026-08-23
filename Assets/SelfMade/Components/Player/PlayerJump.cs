using UnityEngine;

public class PlayerJump : MonoBehaviour
{
  public bool IsOnGround { get; private set; }
  private PlayerInputController playerInputController;

  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    playerInputController.OnJump += HandleJump;
  }
  private void HandleJump()
  {
    Debug.Log("Jump");
  }

  void OnDisable()
  {
    playerInputController.OnJump -= HandleJump;
  }

}