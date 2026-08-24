using UnityEngine;

public class PlatformPassengerDetector : MonoBehaviour
{

  private PlayerMovement playerMovement;
  private PlatformMoving platformMoving;
  private GameObject currentPassenger;
  void Awake()
  {
    platformMoving = GetComponent<PlatformMoving>();
  }
  private void OnCollisionEnter2D(Collision2D collision)
  {


    foreach (ContactPoint2D contact in collision.contacts)
    {

      if (contact.normal.y < 0.5f)
      {

        if (collision.gameObject.tag == "Player")
        {
          playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
          playerMovement.SetCurrentPlatform(platformMoving);
          currentPassenger = collision.gameObject;
        }
        break;
      }
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {

    if (collision.gameObject == currentPassenger)
    {
      currentPassenger = null;
      playerMovement.ClearCurrentPlatform(platformMoving);
      playerMovement = null;
    }
    foreach (ContactPoint2D contact in collision.contacts)
    {

      if (contact.normal.y < 0.5f)
      {
        Debug.Log(collision.gameObject.tag);
        break;
      }
    }
  }
}