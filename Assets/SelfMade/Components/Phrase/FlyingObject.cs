using System;
using DG.Tweening;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 20f;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Collider2D damageCollider;

  public event Action OnFinishPosition;

  private PhraseCollision phraseCollision;
  private Vector3 initialScale;
  private Tween movementTween;
  private bool finished;

  private void Awake()
  {
    phraseCollision = GetComponent<PhraseCollision>();
    phraseCollision.OnReachedTarget += DisableObject;

    if (spriteRenderer == null)
      spriteRenderer = GetComponent<SpriteRenderer>();

    if (damageCollider == null)
      damageCollider = GetComponent<Collider2D>();

    initialScale = transform.localScale;
  }

  public void Prepare()
  {
    finished = false;
    damageCollider.enabled = false;
    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
    transform.localScale = initialScale * 0.6f;
  }

  public void Launch(Vector3 targetPosition)
  {
    damageCollider.enabled = true;

    Sequence launchSequence = DOTween.Sequence().SetLink(gameObject);
    launchSequence.Append(spriteRenderer.DOFade(1f, 0.08f));
    launchSequence.Join(transform.DOScale(initialScale * 1.2f, 0.1f).SetEase(Ease.OutBack));
    launchSequence.Append(transform.DOScale(initialScale, 0.08f));
    launchSequence.AppendCallback(() => StartMovement(targetPosition));
  }

  private void StartMovement(Vector3 targetPosition)
  {
    float duration = Vector3.Distance(transform.position, targetPosition) / Mathf.Max(moveSpeed, 0.01f);

    movementTween = transform
      .DOMove(targetPosition, duration)
      .SetEase(Ease.InQuad)
      .SetLink(gameObject)
      .OnComplete(FinishMovement);
  }

  private void FinishMovement()
  {
    if (finished) return;
    finished = true;
    damageCollider.enabled = false;
    OnFinishPosition?.Invoke();
  }

  private void DisableObject()
  {
    if (finished) return;
    finished = true;
    damageCollider.enabled = false;
    movementTween?.Kill();
  }

  private void OnDisable()
  {
    movementTween?.Kill();

    if (phraseCollision != null)
      phraseCollision.OnReachedTarget -= DisableObject;
  }
}
