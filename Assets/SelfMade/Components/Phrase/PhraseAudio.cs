using UnityEngine;

public class PhraseAudio : MonoBehaviour
{
  private PhraseManager phraseManager;
  public AudioClip audioClip;

  public float volume = 1f;
  public float pitchMin = 0.80f;
  public float pitchMax = 0.95f;

  void Awake()
  {
    phraseManager = GetComponent<PhraseManager>();
    phraseManager.OnSpawnPhrase += PlayPhaseSound;
  }

  private void PlayPhaseSound(Vector3 spawnPos)
  {
    Debug.Log("Play a");
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(0, 0, 0), audioClip, volume, pitch, false);
  }

  void OnDisable()
  {
    phraseManager.OnSpawnPhrase -= PlayPhaseSound;
  }
}