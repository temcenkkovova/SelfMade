using System;
using UnityEngine;

public class PhraseCollision : MonoBehaviour
{

  public event Action OnReachedTarget;
  private bool canDamage = true;
  private Vector3 popupPosition;
  public DeadPopup deadPopupPrefab;

  private string deadMessage;
  void Update()
  {
    if (LevelComponent.Instance.isCompleted) enabled = false;
  }

  public void InitConfig(string deadMessage)
  {
    this.deadMessage = deadMessage;
  }
  public void OnTriggerEnter2D(Collider2D collision)
  {

    if (collision.tag == "Player")
    {
      if (!canDamage) return;
      PlayerLiveState playerLiveState = collision.GetComponent<PlayerLiveState>();
      popupPosition = collision.transform.position;
      DeadPopup popup = Instantiate(deadPopupPrefab, popupPosition, Camera.main.transform.rotation);
      popup.Show(deadMessage, 3f);
      playerLiveState.HandleBladeCollision();
      OnReachedTarget?.Invoke();
      canDamage = false;

      Destroy(gameObject);
    }
  }
}