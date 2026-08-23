using System.Collections;
using UnityEngine;

public class DeadObjectCollision : MonoBehaviour
{
  [SerializeField] private float freezeDelay = 0.2f;

  private Rigidbody2D rb;
  private bool isFreezing;

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

    foreach (ContactPoint2D contact in collision.contacts)
    {

      if (contact.normal.y > 0.5f)
      {
        StartCoroutine(FreezeAfterLanding());
        break;
      }
    }
  }

  private IEnumerator FreezeAfterLanding()
  {
    isFreezing = true;

    yield return new WaitForSeconds(freezeDelay);

    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
    rb.bodyType = RigidbodyType2D.Static;
  }
}