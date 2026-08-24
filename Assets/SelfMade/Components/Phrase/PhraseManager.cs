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


    boxPhrase.Init("TRUST ME", targetPosition);

  }

}