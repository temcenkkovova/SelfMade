using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComponent : MonoBehaviour
{
  private LevelGoal levelGoal;
  public string nextLevelSceneName;
  public bool isCompleted { get; private set; }
  public string finishMessage;
  public Image blackScreenImage;
  public ScreenFade screenFade;
  public DeadPopup finishPopupPrefab;
  public Transform goalTr;
  public event Action OnCompleted;
  public static LevelComponent Instance;

  void Awake()
  {
    isCompleted = false;
    levelGoal = GetComponent<LevelGoal>();
    levelGoal.OnGoalReached += HandleFinishLevel;

    if (Instance != null) Instance = null;
    Instance = this;
  }

  public void HandleFinishLevel()
  {
    if (isCompleted) return;
    OnCompleted?.Invoke();
    isCompleted = true;

    DeadPopup popup = Instantiate(finishPopupPrefab, goalTr.position, Camera.main.transform.rotation);
    popup.Show(finishMessage, 5f);

    screenFade.FadeToBlack().OnComplete(LoadNextLevel);
    // Time.timeScale = 0;
  }

  private void LoadNextLevel()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(nextLevelSceneName);
  }
  void OnDisable()
  {
    levelGoal.OnGoalReached -= HandleFinishLevel;
  }

}