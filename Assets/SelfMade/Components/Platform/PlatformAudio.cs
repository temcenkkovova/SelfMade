using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatformAudio : MonoBehaviour
{
  public AudioClip audioClip;

  public float pitchMin = 0.55f;
  public float pitchMax = 0.75f;
  public float volume = 0.56f;
  private PlatformMoving platformMoving;
  public AudioSource audioSource;
  private bool isPlaying = false;

  void Awake()
  {
    platformMoving = GetComponent<PlatformMoving>();
  }

  void Update()
  {
    if (platformMoving.RBVelocity.sqrMagnitude > 0)
    {
      if (isPlaying) return;
      FadeIn();

    }
    else
    {
      FadeOut();

    }
  }
  [SerializeField, Range(0f, 1f)] private float targetVolume = 0.05f;
  [SerializeField] private float fadeDuration = 0.2f;

  private Coroutine fadeCoroutine;

  public void FadeIn()
  {
    StartFade(FadeInCoroutine());
  }

  public void FadeOut()
  {
    StartFade(FadeOutCoroutine());
  }

  private void StartFade(IEnumerator coroutine)
  {
    if (fadeCoroutine != null)
      StopCoroutine(fadeCoroutine);

    fadeCoroutine = StartCoroutine(coroutine);
  }

  private IEnumerator FadeInCoroutine()
  {
    audioSource.volume = 0f;

    if (!audioSource.isPlaying)
      audioSource.Play();
    isPlaying = true;
    yield return ChangeVolume(targetVolume);

    fadeCoroutine = null;
  }

  private IEnumerator FadeOutCoroutine()
  {
    yield return ChangeVolume(0f);

    audioSource.Stop();
    isPlaying = false;
    fadeCoroutine = null;
  }

  private IEnumerator ChangeVolume(float destination)
  {
    float startVolume = audioSource.volume;
    float elapsed = 0f;

    if (fadeDuration <= 0f)
    {
      audioSource.volume = destination;
      yield break;
    }

    while (elapsed < fadeDuration)
    {
      elapsed += Time.unscaledDeltaTime;

      float progress = Mathf.Clamp01(elapsed / fadeDuration);

      audioSource.volume = Mathf.Lerp(
          startVolume,
          destination,
          progress
      );

      yield return null;
    }

    audioSource.volume = destination;
  }
}