using System;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
  public event Action OnGoalReached;


  public void ReachedGoal()
  {
    OnGoalReached?.Invoke();
  }
}