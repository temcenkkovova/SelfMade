using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
  public List<PlatformMoving> platformsMoving;


  void Start()
  {
    if (platformsMoving == null) return;

    foreach (PlatformMoving platform in platformsMoving)
    {
      platform.StartPlatformMoving();
    }

  }
}