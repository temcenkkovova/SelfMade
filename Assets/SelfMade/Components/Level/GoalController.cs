using System;
using DG.Tweening;
using UnityEngine;

public class GoalController : MonoBehaviour
{
  [Header("References")]
  [SerializeField] private LevelGoal levelGoal;
  [SerializeField] private GoalsManager goalsManager;
  [SerializeField] private SpriteRenderer[] spriteRenderers;
  [SerializeField] private Collider2D goalCollider;

  [Header("Goal")]
  public int numberGoal;

  [Header("Animation")]
  [SerializeField] private float appearDuration = 0.5f;
  [SerializeField] private float disappearDuration = 0.45f;
  [SerializeField] private float disappearRotation = 8f;

  private bool isCollision;
  private Vector3 defaultScale;
  private Quaternion defaultRotation; private void Awake()
  {
    defaultScale = transform.localScale;
    defaultRotation = transform.localRotation;

    if (goalCollider == null)
    {
      goalCollider = GetComponent<Collider2D>();
    }

    if (spriteRenderers == null || spriteRenderers.Length == 0)
    {
      spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.CompareTag("Player")) return;
    if (isCollision) return;

    isCollision = true;

    if (goalCollider != null)
    {
      goalCollider.enabled = false;
    }

    if (numberGoal == 3)
    {
      Disappear(() =>
      {
        levelGoal.PlayDialog();
      });

      return;
    }

    Disappear(() =>
    {
      goalsManager.SpawnNewGoal(numberGoal + 1);
    });
  }
  public void Show()
  {
    gameObject.SetActive(true);

    isCollision = false;

    if (goalCollider != null)
    {
      goalCollider.enabled = false;
    }

    transform.DOKill();

    transform.localScale = defaultScale * 0.65f;
    transform.localRotation = defaultRotation;

    SetAlpha(0f);

    Sequence sequence = DOTween.Sequence();

    sequence.Join(
        transform.DOScale(defaultScale, appearDuration)
            .SetEase(Ease.OutBack)
    );

    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
    {
      sequence.Join(
          spriteRenderer.DOFade(1f, appearDuration)
      );
    }

    sequence.OnComplete(() =>
    {
      if (goalCollider != null)
      {
        goalCollider.enabled = true;
      }
    });
  }
  private void Disappear(Action onComplete)
  {
    transform.DOKill();

    Sequence sequence = DOTween.Sequence();

    sequence.Join(
        transform.DOScale(
            new Vector3(
                defaultScale.x * 1.2f,
                defaultScale.y * 0.05f,
                defaultScale.z
            ),
            disappearDuration
        ).SetEase(Ease.InBack)
    );

    sequence.Join(
        transform.DORotate(
            new Vector3(0f, 0f, disappearRotation),
            disappearDuration
        )
    );

    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
    {
      sequence.Join(
          spriteRenderer.DOFade(0f, disappearDuration)
      );
    }

    sequence.OnComplete(() =>
    {
      gameObject.SetActive(false);
      onComplete?.Invoke();
    });
  }

  private void SetAlpha(float alpha)
  {
    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
    {
      Color color = spriteRenderer.color;
      color.a = alpha;
      spriteRenderer.color = color;
    }
  }

  private void OnDestroy()
  {
    transform.DOKill();

    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
    {
      spriteRenderer.DOKill();
    }
  }
}