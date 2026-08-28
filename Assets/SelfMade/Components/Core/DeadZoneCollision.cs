using UnityEngine;

public class DeadZoneCollision : MonoBehaviour
{
  private bool isDead = false;
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      if (isDead) return;
      PlayerLiveState player = collision.GetComponent<PlayerLiveState>();
      player.PlayerDead();
      isDead = true;
    }
  }

  void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      isDead = false;
    }
  }
}