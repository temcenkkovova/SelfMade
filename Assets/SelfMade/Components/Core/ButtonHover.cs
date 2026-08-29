using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
  [SerializeField] private RectTransform buttonTransform;
  [SerializeField] private float hoverScale = 1.08f;
  [SerializeField] private float duration = 0.15f;
  public TMP_Text textField;
  public Image arrowImage;
  public bool isInteractive;

  private Tween scaleTween;

  public Color disabledColor = Color.gray;

  private void Awake()
  {
    if (buttonTransform == null)
      buttonTransform = transform as RectTransform;

  }
  void Start()
  {

    GetComponent<Button>().interactable = isInteractive;
    if (!isInteractive)
    {
      textField.color = disabledColor;
    }


  }
  public void OnPointerEnter(PointerEventData eventData)
  {
    if (!isInteractive) return;
    scaleTween?.Kill();
    arrowImage.gameObject.SetActive(true);
    scaleTween = buttonTransform
        .DOScale(hoverScale, duration)
        .SetEase(Ease.OutQuad)
        .SetUpdate(true);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (!isInteractive) return;
    scaleTween?.Kill();
    arrowImage.gameObject.SetActive(false);
    scaleTween = buttonTransform
        .DOScale(1f, duration)
        .SetEase(Ease.OutQuad)
        .SetUpdate(true);
  }

  private void OnDisable()
  {
    scaleTween?.Kill();
    buttonTransform.localScale = Vector3.one;
  }
}