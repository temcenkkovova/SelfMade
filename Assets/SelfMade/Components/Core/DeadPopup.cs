using DG.Tweening;
using TMPro;
using UnityEngine;

public class DeadPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private CanvasGroup canvasGroup;

    // [SerializeField] private float lifeTime = 1.5f;
    [SerializeField] private float fadeDuration = 0.35f;
    [SerializeField] private float moveDistance = 1.5f;

    public void Show(string message, float lifeTime)
    {
        damageText.text = message;

        transform.DOKill();
        canvasGroup.DOKill();

        canvasGroup.alpha = 1f;
        transform.localScale = Vector3.one;

        Vector3 startPosition = transform.position;
        Vector3 endPosition =
            startPosition + Vector3.up * moveDistance;

        Sequence sequence = DOTween.Sequence();

        // Движение продолжается всё время жизни.
        sequence.Join(
            transform
                .DOMove(endPosition, lifeTime)
                .SetEase(Ease.OutQuad)

        );

        // Короткий удар при появлении.
        sequence.Insert(
            0f,
            transform.DOPunchScale(
                Vector3.one * 0.15f,
                0.5f,
                5,
                0.5f
            )
        );

        // Исчезновение начинается перед окончанием жизни.
        sequence.Insert(
            lifeTime - fadeDuration,
            canvasGroup
                .DOFade(0f, fadeDuration)
                .SetEase(Ease.InQuad)

        );

        sequence
            .OnComplete(() => Destroy(gameObject))
            .SetLink(gameObject);
    }
}