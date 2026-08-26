using System.Collections;
using UnityEngine;

public class PlayerLiveState : MonoBehaviour
{
  public Animator animator;
  public bool IsDead { get; private set; } = false;

  private PlayerMovement playerMovement;
  public GameObject deadPrefab;
  public GameObject livePrefab;
  private PlayerManager playerManager;
  private Rigidbody2D rb;
  public ParticleSystem bloodSystem;


  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    playerMovement = GetComponent<PlayerMovement>();
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
    Instantiate(bloodSystem, transform);
    StartCoroutine(RespawnCoroutine());


  }

  public IEnumerator RespawnCoroutine()
  {
    yield return new WaitForSeconds(1f);

    Quaternion currentDeadRot = transform.rotation;
    Vector3 currentDeadTr = transform.position;
    if (playerMovement.currentPlatform != null)
    {
      Transform platformTr = playerMovement.currentPlatform.transform;
      GameObject newObject = Instantiate(deadPrefab, currentDeadTr, currentDeadRot);
      newObject.transform.SetParent(platformTr);

      newObject.GetComponent<DeadObjectCollision>().SetPlatformState(true, playerMovement.currentPlatform);

    }
    else
    {

      Instantiate(deadPrefab, currentDeadTr, currentDeadRot);
    }


    Destroy(gameObject);
    playerManager.SpawnPlayer();

  }
}