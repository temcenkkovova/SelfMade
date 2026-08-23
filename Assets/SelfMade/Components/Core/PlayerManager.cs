using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public PlayerBootstrap playerPrefab;
  public GameObject deadPrefab;
  public Transform respawnPos;


  void Start()
  {
    SpawnPlayer();
  }
  public void SpawnPlayer()
  {
    PlayerBootstrap player = Instantiate(playerPrefab, respawnPos.position, respawnPos.rotation);
    player.GetComponent<PlayerLiveState>().Init(this);
  }
}