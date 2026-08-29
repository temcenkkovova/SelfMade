using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
  public event Action OnGoalReached;
  public DialogComponent dialogComponent;
  public CanvasGroup canvasGroup;
  public ScreenFade screenFade;
  private Tween fadeTween;
  public TMP_Text messageField;
  private Tween typingTween;
  [SerializeField] private float secondsPerCharacter = 0.04f;
  public event Action OnFinishDialog;
  public GameObject buttonsEnd;

  public LevelAudio levelAudio;
  public void ReachedGoal()
  {
    OnGoalReached?.Invoke();
  }
  public void PlayDialog()
  {

    dialogComponent.PlayDialog();
    FadeToBlack().OnComplete(PlayEnd);
    GameStateController.Instance.SetState(GameState.GameOver);
  }

  public Tween FadeToBlack()
  {
    fadeTween?.Kill();

    canvasGroup.blocksRaycasts = true;

    fadeTween = canvasGroup
        .DOFade(1f, 8f)
        .SetEase(Ease.InOutQuad)
        .SetUpdate(true);

    return fadeTween;
  }

  private void PlayEnd()
  {
    // Who can you trust? You can’t even trust yourself.
    OnFinishDialog?.Invoke();
    buttonsEnd.SetActive(true);
    typingTween?.Kill();
    messageField.text = "Who can you trust? You can’t even trust yourself.";
    messageField.ForceMeshUpdate();
    messageField.maxVisibleCharacters = 0;
    int characterCount = messageField.textInfo.characterCount;
    float duration = characterCount * secondsPerCharacter;

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


           });
  }
}