using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BoxPhrase : MonoBehaviour
{
  [Header("References")]
  [SerializeField] private TMP_Text textField;
  [SerializeField] private FlyingObject flyingObject;
  [SerializeField] private SpriteRenderer backgroundRenderer;
  [SerializeField] private SpriteRenderer lineRenderer;

  [Header("Timing")]
  [SerializeField] private float appearDuration = 0.4f;
  [SerializeField] private float readingDuration = 1.5f;
  [SerializeField] private float lineDuration = 0.35f;
  [SerializeField] private float warningDuration = 0.55f;
  public event Action OnStartFly;
  private PhraseCollision phraseCollision;
  private Vector3 targetPosition;
  private Vector3 initialScale;
  private Vector3 lineInitialScale;
  private Sequence animationSequence;
  private bool initialized;
  private bool destroyRequested;

  private void Awake()
  {
    initialScale = transform.localScale;
    phraseCollision = flyingObject.GetComponent<PhraseCollision>();

    if (backgroundRenderer == null)
      backgroundRenderer = GetComponent<SpriteRenderer>();

    if (lineRenderer == null)
      lineRenderer = transform.Find("Line")?.GetComponent<SpriteRenderer>();

    if (lineRenderer != null)
      lineInitialScale = lineRenderer.transform.localScale;

    flyingObject.OnFinishPosition += HandleDestroy;
    phraseCollision.OnReachedTarget += HandleDestroy;
  }

  public void Init(string message, string deadMessage, Vector3 targetPosition)
  {
    textField.text = message;
    this.targetPosition = targetPosition;
    initialized = true;
    phraseCollision.InitConfig(deadMessage);
  }

  private void Start()
  {
    if (!initialized)
    {
      Debug.LogError("BoxPhrase must be initialized before its first frame.", this);
      HandleDestroy();
      return;
    }

    PlayAttackSequence();
  }

  private void PlayAttackSequence()
  {

    Color textColor = textField.color;
    Color backgroundColor = backgroundRenderer.color;

    transform.localScale = initialScale * 0.72f;
    textField.alpha = 0f;
    backgroundRenderer.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, 0f);
    flyingObject.Prepare();

    if (lineRenderer != null)
    {
      Color lineColor = lineRenderer.color;
      lineRenderer.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0f);
      lineRenderer.transform.localScale = new Vector3(0f, lineInitialScale.y, lineInitialScale.z);
    }

    animationSequence = DOTween.Sequence().SetLink(gameObject);
    animationSequence.Append(transform.DOScale(initialScale * 1.08f, appearDuration).SetEase(Ease.OutBack));
    animationSequence.Join(backgroundRenderer.DOFade(backgroundColor.a, appearDuration));
    animationSequence.Append(transform.DOScale(initialScale, 0.12f).SetEase(Ease.OutQuad));
    animationSequence.Join(textField.DOFade(textColor.a, 0.18f));
    animationSequence.AppendInterval(readingDuration);

    if (lineRenderer != null)
    {
      animationSequence.Append(lineRenderer.DOFade(1f, 0.08f));
      animationSequence.Join(lineRenderer.transform.DOScaleX(lineInitialScale.x, lineDuration).SetEase(Ease.OutCubic));
      animationSequence.Append(lineRenderer.DOFade(0.35f, 0.12f));
      animationSequence.Append(lineRenderer.DOFade(1f, 0.12f));
    }

    animationSequence.AppendInterval(warningDuration);
    StartCoroutine(test(warningDuration));
    animationSequence.Append(transform.DOPunchPosition(Vector3.right * 0.12f, 0.22f, 7, 0.5f));
    animationSequence.AppendCallback(() => flyingObject.Launch(targetPosition));
  }
  private IEnumerator test(float delay)
  {
    yield return new WaitForSeconds(delay + 2.3f);
    OnStartFly?.Invoke();
  }
  public void HandleDestroy()
  {
    if (destroyRequested) return;
    destroyRequested = true;
    animationSequence?.Kill();

    Sequence outro = DOTween.Sequence().SetLink(gameObject);
    outro.Join(backgroundRenderer.DOFade(0f, 0.35f));
    outro.Join(textField.DOFade(0f, 0.25f));

    if (lineRenderer != null)
      outro.Join(lineRenderer.DOFade(0f, 0.25f));

    outro.OnComplete(() => Destroy(gameObject));
  }

  private void OnDestroy()
  {
    animationSequence?.Kill();

    if (phraseCollision != null)
      phraseCollision.OnReachedTarget -= HandleDestroy;

    if (flyingObject != null)
      flyingObject.OnFinishPosition -= HandleDestroy;
  }
}
