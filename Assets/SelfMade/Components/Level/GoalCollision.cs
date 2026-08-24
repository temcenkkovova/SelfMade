using UnityEngine;

public class GoalCollision : MonoBehaviour
{
  public LevelGoal levelGoal;

  private bool isCollision = false;

  public void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      if (isCollision) return;
      levelGoal.ReachedGoal();
      isCollision = true;
    }
  }
}