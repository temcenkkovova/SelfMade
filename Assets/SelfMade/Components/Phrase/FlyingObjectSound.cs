using UnityEngine;

public class FlyingObjectSound : MonoBehaviour
{
  public AudioClip audioClip;
  public float volume = 1f;
  public float pitchMin = 0.80f;
  public float pitchMax = 0.95f;
  private BoxPhrase boxPhrase;

  void Awake()
  {
    boxPhrase = GetComponent<BoxPhrase>();
    boxPhrase.OnStartFly += PlayFlySound;
  }
  private void PlayFlySound()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(transform.position, audioClip, 0.5f, pitch, false);
  }

  void OnDisable()
  {
    boxPhrase.OnStartFly -= PlayFlySound;
  }
}