using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class ActivePlatformButton : MonoBehaviour
{

  public PlatformMoving platformMoving;


  private readonly HashSet<Collider2D> objectsOnButton = new();
  private bool isCorpse = false;

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (!IsValidObject(collision))
      return;

    objectsOnButton.Add(collision);
    if (collision.tag == "Corpse")
    {
      isCorpse = true;
    }
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

    if (objectsOnButton.Count > 0 || isCorpse)
    {
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

  void OnDisable()
  {

  }
}