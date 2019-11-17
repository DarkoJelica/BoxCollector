using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

   public void LoadGame()
   {
      SceneManager.LoadSceneAsync(1);
   }

   public void Quit()
   {
      Application.Quit();
   }

}
