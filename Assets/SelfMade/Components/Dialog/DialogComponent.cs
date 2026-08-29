using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DialogComponent : MonoBehaviour
{


  public float delayMessage;
  public float startDelay = 2f;
  public List<string> messages = new List<string>();

  public event Action OnPlayDialog;
  public event Action<string, int> OnNewMessage;

  private int currentMessageCount = 0;

  public void PlayDialog()
  {
    OnPlayDialog?.Invoke();

    StartCoroutine(MessageCoroutine());
  }

  private IEnumerator MessageCoroutine()
  {
    yield return new WaitForSeconds(startDelay);
    OnNewMessage?.Invoke(messages[currentMessageCount], currentMessageCount);
    currentMessageCount++;
    while (currentMessageCount <= messages.Count - 1)
    {
      yield return new WaitForSeconds(delayMessage);
      OnNewMessage?.Invoke(messages[currentMessageCount], currentMessageCount);
      currentMessageCount++;
    }


  }

}