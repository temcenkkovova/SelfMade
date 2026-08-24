using DG.Tweening;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform endPoint;

  [SerializeField] private float moveDuration = 2f;
  [SerializeField] private float waitDuration = 0.5f;
  public Vector2 Velocity { get; private set; }
  private Vector2 previousPosition;
  private Sequence movementSequence;


  private void Start()
  {
    previousPosition = transform.position;
    Vector2 startPosition = rb.position;
    Vector2 endPosition = endPoint.position;

    movementSequence = DOTween.Sequence();

    movementSequence
        // Движение к конечной точке.
        .Append(
            rb.DOMove(endPosition, moveDuration)
                .SetEase(Ease.Linear)
        )

        // Остановка в конечной точке.
        .AppendInterval(waitDuration)

        // Возвращение назад.
        .Append(
            rb.DOMove(startPosition, moveDuration)
                .SetEase(Ease.Linear)
        )

        // Остановка в начальной точке.
        .AppendInterval(waitDuration)

        // Бесконечное повторение.
        .SetLoops(-1, LoopType.Restart)

        // Работа в цикле физики.
        .SetUpdate(UpdateType.Fixed)

        // Уничтожение Sequence вместе с платформой.
        .SetLink(gameObject);
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