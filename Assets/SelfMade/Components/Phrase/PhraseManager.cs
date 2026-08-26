using System;
using System.Collections;
using UnityEngine;

public class PhraseManager : MonoBehaviour
{
  public BoxPhrase boxPhrasePrefab;
  public GameObject linePrefab;
  public Transform startPosition;
  public Transform finishPosition;
  public Vector2 fixedPlayerVector;

  [SerializeField] private float firstSpawnDelay = 2f;
  [SerializeField] private float flyingCooldown = 10f;
  public event Action<Vector3> OnSpawnPhrase;

  public Phrases phrasesConfig;

  private int currentPhraseIndex = 0;


  void Start()
  {
    StartCoroutine(SpawnPhrasesCoroutine());
  }

  public IEnumerator SpawnPhrasesCoroutine()
  {
    yield return new WaitForSeconds(firstSpawnDelay);
    while (true)
    {
      SpawnPhrase();
      yield return new WaitForSeconds(flyingCooldown);
    }

  }

  public void SpawnPhrase()
  {
    if (LevelComponent.Instance.isCompleted) return;
    GameObject player =
          GameObject.FindGameObjectWithTag("Player");
    if (player == null)
    {
      return;
    }
    float capturedPlayerY = player.transform.position.y;
    Vector3 spawnPosition = new Vector3(startPosition.position.x, capturedPlayerY, 0f);

    Vector3 targetPosition = new Vector3(finishPosition.position.x, capturedPlayerY, 0f);

    BoxPhrase boxPhrase = Instantiate(boxPhrasePrefab, spawnPosition, Quaternion.identity);
    OnSpawnPhrase?.Invoke(boxPhrase.transform.position);

    boxPhrase.Init(GetCurrentPhrase(), GetCurrentDeadPhrase(), targetPosition);
    ChangePhraseIndex();
  }

  public string GetCurrentPhrase()
  {
    string newPhrase;
    if (currentPhraseIndex > phrasesConfig.levelBoxPhrases.Count)
    {
      newPhrase = phrasesConfig.levelBoxPhrases[0];
      return newPhrase;
    }


    newPhrase = phrasesConfig.levelBoxPhrases[currentPhraseIndex];
    return newPhrase;
  }

  public string GetCurrentDeadPhrase()
  {
    string deadPhrase;
    if (currentPhraseIndex > phrasesConfig.levelBoxPhrases.Count)
    {
      deadPhrase = phrasesConfig.levelDeadPhrases[0];
      return deadPhrase;
    }


    deadPhrase = phrasesConfig.levelDeadPhrases[currentPhraseIndex];
    return deadPhrase;
  }
  public void ChangePhraseIndex()
  {
    if (currentPhraseIndex >= phrasesConfig.levelBoxPhrases.Count)
    {
      currentPhraseIndex = 0;

    }
    else
    {
      currentPhraseIndex++;
    }
  }

}