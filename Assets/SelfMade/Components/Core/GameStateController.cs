using System;

using UnityEngine;
public enum GameState { Gameplay, Pause, Dialogue, GameOver, Menu }
public class GameStateController : MonoBehaviour
{
  [SerializeField] private GameState initialState = GameState.Gameplay;
  public static GameStateController Instance;
  public GameState CurrentState { get; private set; }

  public event Action<GameState> OnGameStateChanged;

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }
  void Start()
  {
    SetState(initialState);
  }
  public void SetState(GameState newState)
  {
    if (CurrentState == newState) return;
    CurrentState = newState;
    OnGameStateChanged?.Invoke(CurrentState);

  }
  public void GameOver()
  {
    SetState(GameState.GameOver);
  }
  public bool IsGameplayState()
  {
    return CurrentState == GameState.Gameplay;
  }
}