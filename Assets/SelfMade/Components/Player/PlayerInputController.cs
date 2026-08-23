using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerInputController : MonoBehaviour
{
  private PlayerInput inputActions;
  public Vector2 MoveInput { get; private set; }
  public event Action OnJump;

  void Awake()
  {
    inputActions = new PlayerInput();
  }

  void Update()
  {
    MoveInput = inputActions.Player.Move.ReadValue<Vector2>();


  }
  void OnEnable()
  {
    inputActions.Player.Enable();
    inputActions.Player.Jump.performed += OnJumpStarted;
  }
  void OnDisable()
  {
    inputActions.Player.Disable();
    inputActions.Player.Jump.performed -= OnJumpStarted;
  }


  public void OnJumpStarted(InputAction.CallbackContext context) => OnJump?.Invoke();
}