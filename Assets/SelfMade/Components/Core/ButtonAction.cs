
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonAction : MonoBehaviour
{


  public void BeginClick()
  {
    SceneManager.LoadScene("FirstLevel");
  }

  public void ContinueClick()
  {

  }

  public void ExitClick()
  {
    Application.Quit();
  }

  public void ToMenuClick()
  {
    SceneManager.LoadScene("Menu");
  }
}