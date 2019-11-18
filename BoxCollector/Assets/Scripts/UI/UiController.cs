using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour {

   public MenuController Menu;
   public HudController HUD;
   public InventoryController Inventory;

   enum UiModes { MENU, HUD, INVENTORY }

   UiModes currentMode = UiModes.HUD;
   bool initalized = false;

   void Update()
   {
      PlayerController player = PlayerController.PlayerInstance;
      if(PlayerController.PlayerInstance == null)
      {
         currentMode = UiModes.MENU;
         if(initalized)
            Menu.Title.text = "You died";
         else if(Menu.Title != null)
            Menu.Title.text = "Box Collector";
      }
      else
      {
         if(!initalized)
            initalized = true;
         if(player.Boxes >= Collectible.BoxCount && EnemyController.EnemyCount == 0)
         {
            currentMode = UiModes.MENU;
            Menu.Title.text = "You are victorious";
         }
         else
         {
            Menu.Title.text = "Box Collector";
            if(Input.GetKeyDown(KeyCode.Escape))
               currentMode = currentMode == UiModes.MENU ? UiModes.HUD : UiModes.MENU;
            if(currentMode != UiModes.MENU && Input.GetKeyDown(KeyCode.Tab))
               currentMode = currentMode == UiModes.INVENTORY ? UiModes.HUD : UiModes.INVENTORY;
         }
      }
      switch(currentMode)
      {
         case UiModes.MENU:
            Menu.gameObject.SetActive(true);
            HUD.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(false);
            Menu.Start.text = initalized ? "Restart" : "Start";
            break;
         case UiModes.HUD:
            Menu.gameObject.SetActive(false);
            HUD.gameObject.SetActive(true);
            Inventory.gameObject.SetActive(false);
            break;
         case UiModes.INVENTORY:
            Menu.gameObject.SetActive(false);
            HUD.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(true);
            break;
      }
      Cursor.visible = currentMode != UiModes.HUD;
      if(player != null)
         player.BlockShooting = currentMode != UiModes.HUD;
   }

}
