using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
  public AudioClip jumpSound;
  public AudioClip punchedClip;

  public AudioClip reviveClip;

  public float volume = 0.7f;
  public float pitchMin = 0.80f;
  public float pitchMax = 0.95f;

  private PlayerJump playerJump;

  private PlayerLiveState playerLiveState;

  void Awake()
  {
    playerJump = GetComponent<PlayerJump>();

    playerLiveState = GetComponent<PlayerLiveState>();
    playerJump.OnJump += PlayJumpSound;
    playerLiveState.OnRevive += PlayReviveSound;

  }

  private void PlayJumpSound()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(transform.position, jumpSound, 0.5f, pitch, false);
  }

  public void PlayPunchedSound()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(transform.position, punchedClip, 0.5f, pitch, false);
  }
  private void PlayReviveSound(Vector3 spawnPos)
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(spawnPos, reviveClip, 0.5f, pitch, false);
  }
  void OnDestroy()
  {
    playerJump.OnJump -= PlayJumpSound;
    playerLiveState.OnRevive -= PlayReviveSound;

  }

}