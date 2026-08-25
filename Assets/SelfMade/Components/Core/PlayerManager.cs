using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public PlayerBootstrap playerPrefab;
  public GameObject deadPrefab;
  public Transform respawnPos;
  public PhraseManager phraseManager;

  void Start()
  {
    SpawnPlayer();
  }
  public void SpawnPlayer()
  {
    PlayerBootstrap player = Instantiate(playerPrefab, respawnPos.position, respawnPos.rotation);
    Debug.Log(player.transform.position);
    player.GetComponent<PlayerLiveState>().Init(this);

  }
}