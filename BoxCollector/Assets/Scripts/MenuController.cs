using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

   public Text Title;
   public Text Start;

   public void LoadGame()
   {
      SceneManager.LoadSceneAsync(1);
   }

   public void Quit()
   {
      Application.Quit();
   }

}
