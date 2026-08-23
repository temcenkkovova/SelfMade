using UnityEngine;



public class PlayerInputController : MonoBehaviour
{
  private PlayerInput inputActions;
  public Vector2 MoveInput { get; private set; }

  void Awake()
  {
    inputActions = new PlayerInput();
  }

  void Update()
  {
    MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
  }
  void OnDisable()
  {
    inputActions.Player.Disable();
  }

  void OnEnable()
  {
    inputActions.Player.Enable();
  }
}