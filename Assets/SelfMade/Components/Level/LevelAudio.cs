using UnityEngine;

public class LevelAudio : MonoBehaviour
{
  public AudioClip levelBackgroundAudio;
  public AudioClip levelFinishAudio;
  public float pitchMin = 0.75f;
  public float pitchMax = 0.95f;
  public float volume = 0.76f;
  public AudioClip disconnectedClip;

  public float volumeFinishAudio;
  private LevelGoal levelGoal;
  public AudioSource backgroundAudioSource;


  void Awake()
  {
    levelGoal = GetComponent<LevelGoal>();
    levelGoal.OnGoalReached += PlayFinishSound;

    levelGoal.OnFinishDialog += PlayFinishClip;
  }
  void Start()
  {
    GameStateController.Instance.OnGameStateChanged += PlayDisconnected;
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(0, 0, 0), levelBackgroundAudio, volume, pitch, true);
  }

  void OnDisable()
  {
    levelGoal.OnGoalReached -= PlayFinishSound;
    GameStateController.Instance.OnGameStateChanged -= PlayDisconnected;
    levelGoal.OnFinishDialog -= PlayFinishClip;
  }

  private void PlayFinishSound()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(0, 0, 0), levelFinishAudio, volumeFinishAudio, pitch, false);
  }

  private void PlayDisconnected(GameState gameState)
  {
    if (gameState == GameState.GameOver)
    {
      backgroundAudioSource.volume = 0f;

    }


  }

  public void PlayFinishClip()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(0, 0, 0), disconnectedClip, volumeFinishAudio, pitch, false);
  }
}