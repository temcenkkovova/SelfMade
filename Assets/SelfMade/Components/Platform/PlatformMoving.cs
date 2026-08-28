using System;
using DG.Tweening;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform endPoint;

  [SerializeField] private float moveDuration = 4f;
  [SerializeField] private float waitDuration = 0.5f;
  public Vector2 Velocity { get; private set; }
  private Vector2 previousPosition;
  private Sequence movementSequence;
  public bool IsActivatedPlatform = false;
  private Vector2 startPosition;

  private bool isPlatformMoving = false;
  public event Action OnStartPos;

  private void Start()
  {
    startPosition = rb.position;
    previousPosition = rb.position;
  }
  public void StartPlatformMoving()
  {
    if (IsActivatedPlatform)
    {
      Debug.Log("Activated");
      return;
    }
    movementSequence?.Kill();
    movementSequence = null;

    Vector2 endPosition = endPoint.position;
    IsActivatedPlatform = true;
    movementSequence = DOTween.Sequence();

    movementSequence
        .Append(
            rb.DOMove(endPosition, moveDuration)
                .SetEase(Ease.Linear)
        )
        .AppendInterval(waitDuration)
        .Append(
            rb.DOMove(startPosition, moveDuration)
                .SetEase(Ease.Linear)
        )
        .AppendInterval(waitDuration)
        .SetLoops(-1, LoopType.Restart)
        .SetUpdate(UpdateType.Fixed)
        .SetLink(gameObject);
  }

  public void DeactivatedPlatform()
  {
    if (!IsActivatedPlatform)
    {
      Debug.Log("Deactivate");
      return;
    }
    movementSequence?.Kill();
    movementSequence = null;
    movementSequence = DOTween.Sequence();

    movementSequence
        .Append(
            rb.DOMove(startPosition, GetReturnDuration())
                .SetEase(Ease.Linear)
        )
        .SetUpdate(UpdateType.Fixed)
        .OnComplete(() => HandleStartPos())
        .SetLink(gameObject);
  }
  private float GetReturnDuration()
  {
    float fullDistance =
        Vector2.Distance(startPosition, endPoint.position);

    float remainingDistance =
        Vector2.Distance(rb.position, startPosition);

    if (fullDistance <= 0.001f)
      return 0f;

    // Платформа возвращается с той же скоростью,
    // даже если остановилась посередине маршрута.
    return moveDuration * (remainingDistance / fullDistance);
  }

  private void HandleStartPos()
  {
    IsActivatedPlatform = false;
    // IsOnStartPos = true;
    OnStartPos?.Invoke();
  }
  private void FixedUpdate()
  {
    Vector2 currentPosition = rb.position;

    Velocity =
        (currentPosition - previousPosition)
        / Time.fixedDeltaTime;

    previousPosition = currentPosition;
  }
  private void OnDestroy()
  {
    movementSequence?.Kill();
  }
}