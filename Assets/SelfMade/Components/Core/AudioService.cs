
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AudioService : MonoBehaviour
{
  public static AudioService Instance;
  public Transform poolParent;
  [SerializeField] private AudioSource prefab;
  private Queue<AudioSource> pool = new();

  public void Awake()
  {
    Instance = this;
  }

  public void PlayAt(Vector3 pos, AudioClip clip, float volume, float pitch, bool loopState)
  {
    var src = Get();
    src.transform.position = pos;
    src.clip = clip;
    src.volume = volume;
    src.pitch = pitch;
    src.spatialBlend = 1f; // 3D
    src.Play();
    src.loop = loopState;
    StartCoroutine(Release(src));
  }
  AudioSource Get()
  {
    return pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, poolParent);
  }

  IEnumerator Release(AudioSource src)
  {
    yield return new WaitWhile(() => src.isPlaying);
    pool.Enqueue(src);
  }
}