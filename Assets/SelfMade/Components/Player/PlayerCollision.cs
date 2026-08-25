using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  private PlayerMovement playerMovement;
  public LayerMask layerCorpse;
  private PlatformMoving currentPlatform;
  void Awake()
  {
    playerMovement = GetComponent<PlayerMovement>();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {

    Debug.Log(collision.gameObject.tag);

    if (collision.gameObject.tag == "Corpse")
    {

      DeadObjectCollision deadObject = collision.gameObject.GetComponent<DeadObjectCollision>();
      if (deadObject.isOnPlatform)
      {

        playerMovement.SetCurrentPlatform(deadObject.currentPlatform);
        currentPlatform = deadObject.currentPlatform;
      }
    }
    if (collision.gameObject.tag == "Platform")
    {
      PlatformMoving platformMoving = collision.gameObject.GetComponent<PlatformMoving>();
      playerMovement.SetCurrentPlatform(platformMoving);
      currentPlatform = platformMoving;
      Debug.Log(currentPlatform);

    }
    if (collision.gameObject.tag == "Ground")
    {
      if (currentPlatform == null) return;
      playerMovement.ClearCurrentPlatform(currentPlatform);

    }


  }
  // private void OnCollisionExit2D(Collision2D other)
  // {
  //   if (other.gameObject.tag == "Corpse")
  //   {
  //     playerMovement.ClearCurrentPlatform(currentPlatform);
  //     currentPlatform = null;
  //   }
  // }

}