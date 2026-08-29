using DG.Tweening;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{

  public CanvasGroup canvasGroup;
  public float fadeDuration = 2f;

  private Tween fadeTween;

  void Awake()
  {
    canvasGroup.alpha = 1f;
  }
  void Start()
  {
    FadeFromBlack();
  }

  public void FadeFromBlack()
  {
    fadeTween?.Kill();

    canvasGroup.alpha = 1f;
    canvasGroup.blocksRaycasts = true;

    fadeTween = canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InFlash).SetUpdate(true).OnComplete(() => canvasGroup.blocksRaycasts = false);

  }
  public Tween FadeToBlack(float duration)
  {
    fadeTween?.Kill();

    canvasGroup.blocksRaycasts = true;

    fadeTween = canvasGroup
        .DOFade(1f, fadeDuration)
        .SetEase(Ease.InOutQuad)
        .SetUpdate(true);

    return fadeTween;
  }
  private void OnDestroy()
  {
    fadeTween?.Kill();
  }
}