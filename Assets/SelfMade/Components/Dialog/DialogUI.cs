using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
  public TMP_Text messageField;

  public GameObject dialogPanel;

  public DialogComponent dialogComponent;
  public GameObject redBorderFirst;
  public GameObject redBorderSecond;
  public TMP_Text nameOperatorField;
  [SerializeField] private float secondsPerCharacter = 0.04f;
  public LevelGoal levelGoal;

  private Tween typingTween;

  public bool IsTyping { get; private set; }

  void Start()
  {
    dialogComponent.OnPlayDialog += ShowDialog;
    dialogComponent.OnNewMessage += ShowMessage;
    levelGoal.OnFinishDialog += HideDialog;
  }
  public void ShowDialog()
  {

    dialogPanel.SetActive(true);
  }
  public void HideDialog()
  {
    dialogPanel.SetActive(false);
  }

  public void ShowMessage(string message, int numberField)
  {
    typingTween?.Kill();
    messageField.text = message;
    messageField.ForceMeshUpdate();
    messageField.maxVisibleCharacters = 0;
    int characterCount = messageField.textInfo.characterCount;
    float duration = characterCount * secondsPerCharacter;
    IsTyping = true;
    typingTween = DOTween
           .To(
               () => messageField.maxVisibleCharacters,
               value => messageField.maxVisibleCharacters = value,
               characterCount,
               duration
           )
           .SetEase(Ease.Linear)
           .SetUpdate(true)
           .OnComplete(() =>
           {
             messageField.maxVisibleCharacters = characterCount;
             IsTyping = false;
           });
    if (numberField == 0 || numberField == 2)
    {
      redBorderSecond.SetActive(false);
      redBorderFirst.SetActive(true);
      nameOperatorField.text = "OPERATOR 1";
    }
    if (numberField == 1 || numberField == 3)
    {
      redBorderFirst.SetActive(false);
      redBorderSecond.SetActive(true);
      nameOperatorField.text = "OPERATOR 2";
    }


  }

  void OnDisable()
  {
    dialogComponent.OnPlayDialog -= ShowDialog;
    dialogComponent.OnNewMessage -= ShowMessage;
    levelGoal.OnFinishDialog -= HideDialog;
    typingTween?.Kill();
    IsTyping = false;
  }
  public void CompleteImmediately()
  {
    if (!IsTyping) return;

    typingTween?.Complete();
  }


}