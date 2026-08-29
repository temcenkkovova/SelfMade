using System.Collections.Generic;
using UnityEngine;

public class GoalsManager : MonoBehaviour
{
  public List<GoalController> goals;

  public GoalController CurrentActiveGoal { get; private set; }


  void Start()
  {
    foreach (GoalController goal in goals)
    {
      goal.gameObject.SetActive(false);
    }
    SpawnNewGoal(1);

  }

  public void SpawnNewGoal(int numberGoal)
  {
    GoalController newGoal = goals.Find(
          goal => goal.numberGoal == numberGoal
      );

    if (newGoal == null)
    {
      Debug.LogWarning($"Goal number {numberGoal} not found.");
      return;
    }

    CurrentActiveGoal = newGoal;
    CurrentActiveGoal.Show();

  }

}