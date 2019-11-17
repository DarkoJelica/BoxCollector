using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

   public Text Health;
   public Text Ammo;
   public Text Boxes;
	
	void OnGUI() {
      if(PlayerController.PlayerInstance == null)
         return;
      if(Health != null)
         Health.text = "Health: " + Mathf.RoundToInt(PlayerController.PlayerInstance.Health.CurrentHealth) + '/' + Mathf.RoundToInt(PlayerController.PlayerInstance.Health.Health);
      if(Ammo != null)
         Ammo.text = "Ammo: " + PlayerController.PlayerInstance.Ammo;
      if(Boxes != null)
         Boxes.text = "Boxes: " + PlayerController.PlayerInstance.Boxes;
	}
}
