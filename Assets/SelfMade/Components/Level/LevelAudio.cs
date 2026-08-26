using UnityEngine;

public class LevelAudio : MonoBehaviour
{
  public AudioClip levelBackgroundAudio;
  public AudioClip levelFinishAudio;
  public float pitchMin = 0.75f;
  public float pitchMax = 0.95f;
  public float volume = 0.76f;

  public float volumeFinishAudio;
  private LevelGoal levelGoal;

  void Awake()
  {
    levelGoal = GetComponent<LevelGoal>();
    levelGoal.OnGoalReached += PlayFinishSound;
  }
  void Start()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(0, 0, 0), levelBackgroundAudio, volume, pitch, true);
  }

  void OnDisable()
  {
    levelGoal.OnGoalReached -= PlayFinishSound;
  }

  private void PlayFinishSound()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(0, 0, 0), levelFinishAudio, volumeFinishAudio, pitch, false);
  }
}