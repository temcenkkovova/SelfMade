using UnityEngine;

public class BladeCollision : MonoBehaviour
{


  public void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      PlayerLiveState playerLiveState = collision.GetComponent<PlayerLiveState>();
      playerLiveState.HandleBladeCollision();
    }
  }
}