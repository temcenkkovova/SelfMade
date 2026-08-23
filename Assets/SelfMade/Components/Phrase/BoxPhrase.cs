using System.Collections;
using TMPro;
using UnityEngine;

public class BoxPhrase : MonoBehaviour
{
  public TMP_Text textField;
  public FlyingObject flyingObject;
  private PhraseCollision phraseCollision;
  private Vector3 targetPosition;

  public void Init(string message, Vector3 targetPosition)
  {
    textField.text = message;
    this.targetPosition = targetPosition;
  }
  void Start()
  {
    StartCoroutine(StartMovingCoroutine());
    phraseCollision = flyingObject.GetComponent<PhraseCollision>();
    phraseCollision.OnReachedTarget += HandleDestroy;
  }

  private IEnumerator StartMovingCoroutine()
  {
    yield return new WaitForSeconds(2f);
    flyingObject.SetTarget(targetPosition);
  }
  void OnDestroy()
  {
    phraseCollision.OnReachedTarget -= HandleDestroy;
  }
  public void HandleDestroy()
  {
    Destroy(gameObject, 2f);
  }
}