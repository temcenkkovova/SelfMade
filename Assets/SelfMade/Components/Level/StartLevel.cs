using UnityEngine;

public class StartLevel : MonoBehaviour
{
  public PlatformMoving platformMoving;


  void Start()
  {
    if (platformMoving == null) return;
    platformMoving.StartPlatformMoving();
  }
}