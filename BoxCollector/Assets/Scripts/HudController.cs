using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

   public Text Health;
   public Text Ammo;
   public Text Boxes;
   public Text Pickup;
	
	void OnGUI() {
      PlayerController player = PlayerController.PlayerInstance;
      if(player == null)
         return;
      if(Health != null)
         Health.text = "Health: " + Mathf.RoundToInt(player.Health.CurrentHealth) + '/' + Mathf.RoundToInt(player.Health.MaxHealth);
      if(Ammo != null)
         Ammo.text = "Ammo: " + PlayerController.PlayerInstance.Ammo;
      if(Boxes != null)
         Boxes.text = "Boxes: " + PlayerController.PlayerInstance.Boxes + '/' + Collectible.BoxCount;
      if(Pickup != null)
      {
         if(PlayerController.PlayerInstance.PickupObject == null)
            Pickup.gameObject.SetActive(false);
         else
         {
            Pickup.gameObject.SetActive(true);
            Pickup.text = "[E] Pickup " + PlayerController.PlayerInstance.PickupObject.name;
         }
      }
	}
}
