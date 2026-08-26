using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Phrases")]
public class Phrases : ScriptableObject
{

  public List<string> levelBoxPhrases;
  public List<string> levelDeadPhrases;
  public string levelGoalPhrase;
}