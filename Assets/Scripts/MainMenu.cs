using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayOnline()
   {
        SceneManager.LoadScene(1);
   }

   public void PlayVsAi()
   {
      SceneManager.LoadScene(2);
   }

   public void QuitButton()
   {
      Application.Quit();
   }
}