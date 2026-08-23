using TMPro;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{

  private Vector3 targetPosition;
  [SerializeField] private float moveSpeed = 20f;
  private bool isInitialized = false;
  private PhraseCollision phraseCollision;

  private bool canDamage;

  void Awake()
  {
    phraseCollision = GetComponent<PhraseCollision>();
    phraseCollision.OnReachedTarget += DisabledObject;
  }
  void Update()
  {
    if (!isInitialized) return;

    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
    {
      Destroy(gameObject);
    }
  }
  public void SetTarget(Vector3 targetPosition)
  {

    this.targetPosition = targetPosition;
    isInitialized = true;
  }

  public void DisabledObject()
  {

  }

  void OnDisable()
  {
    phraseCollision.OnReachedTarget -= DisabledObject;
  }
}