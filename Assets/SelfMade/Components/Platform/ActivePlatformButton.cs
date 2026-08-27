using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class ActivePlatformButton : MonoBehaviour
{

  public PlatformMoving platformMoving;

  private bool activated = false;
  private readonly HashSet<Collider2D> objectsOnButton = new();

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (!IsValidObject(collision))
      return;

    objectsOnButton.Add(collision);
  }

  void OnTriggerExit2D(Collider2D collision)
  {
    if (!IsValidObject(collision))
      return;

    objectsOnButton.Remove(collision);
  }
  private void FixedUpdate()
  {

    objectsOnButton.RemoveWhere(collider =>
        collider == null ||
        !collider.enabled ||
        !collider.gameObject.activeInHierarchy
    );

    bool shouldBeActivated = objectsOnButton.Count > 0;

    if (shouldBeActivated == activated)
      return;

    activated = shouldBeActivated;

    if (activated)
    {
      Debug.Log(objectsOnButton.Count);
      platformMoving.StartPlatformMoving();
    }
    else
    {
      platformMoving.DeactivatedPlatform();
    }
  }
  private bool IsValidObject(Collider2D other)
  {
    return other.CompareTag("Player") ||
           other.CompareTag("Corpse");
  }
}