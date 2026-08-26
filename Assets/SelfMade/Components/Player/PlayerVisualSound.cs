using UnityEngine;

public class PlayerVisualSound : MonoBehaviour
{
  public float volume = 0.7f;
  public float pitchMin = 0.80f;
  public float pitchMax = 0.95f;
  public AudioClip walkSound;
  public void PlayMovementSound()
  {

    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(transform.position, walkSound, 0.5f, pitch, false);
  }
}