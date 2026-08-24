using System.Collections;
using UnityEngine;

public class PlayerLiveState : MonoBehaviour
{
  public Animator animator;
  public bool IsDead { get; private set; } = false;


  public GameObject deadPrefab;
  public GameObject livePrefab;
  private PlayerManager playerManager;
  private Rigidbody2D rb;


  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  public void Init(PlayerManager playerManager)
  {
    this.playerManager = playerManager;
  }
  public void HandleBladeCollision()
  {
    if (LevelComponent.Instance.isCompleted) return;
    if (IsDead) return;
    IsDead = true;
    animator.SetTrigger("Dead");

    StartCoroutine(RespawnCoroutine());


  }

  public IEnumerator RespawnCoroutine()
  {
    yield return new WaitForSeconds(1f);
    Vector3 currentDeadTr = transform.position;
    Quaternion currentDeadRot = transform.rotation;
    Instantiate(deadPrefab, currentDeadTr, currentDeadRot);
    Destroy(gameObject);
    playerManager.SpawnPlayer();

  }
}