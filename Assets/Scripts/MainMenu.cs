using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayOnline()
   {
        SceneManager.LoadScene("Game");
   }

   public void PlayVsAi()
   {
      SceneManager.LoadScene(4);
   }

   public void QuitButton()
   {
      Application.Quit();
   }
}
