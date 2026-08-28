using System.Collections;
using UnityEngine;

public class DeadObjectCollision : MonoBehaviour
{
  [SerializeField] private float freezeDelay = 0.2f;

  private Rigidbody2D rb;
  private bool isFreezing;
  public bool isOnPlatform { get; private set; } = false;
  public PlatformMoving currentPlatform { get; private set; }
  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (isFreezing)
    {
      return;
    }

    if (collision.gameObject.tag == "Platform")
    {
      if (currentPlatform == null) // тут сменил с != null
        currentPlatform = collision.gameObject.GetComponent<PlatformMoving>();
    }


    foreach (ContactPoint2D contact in collision.contacts)
    {

      if (contact.normal.y > 0.5f)
      {
        StartCoroutine(FreezeAfterLanding());
        break;
      }
    }
  }

  public void SetPlatformState(bool newState, PlatformMoving platform)
  {
    isOnPlatform = newState;
    currentPlatform = platform;
  }
  private IEnumerator FreezeAfterLanding()
  {
    isFreezing = true;

    yield return new WaitForSeconds(freezeDelay);

    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
    isFreezing = false;
    if (isOnPlatform)
    {
      rb.bodyType = RigidbodyType2D.Kinematic;
    }
    else
    {
      rb.bodyType = RigidbodyType2D.Static;
    }

  }
}