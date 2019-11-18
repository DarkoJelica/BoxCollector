using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour {

   public MenuController Menu;
   public HudController HUD;
   public InventoryController Inventory;

   enum UiModes { MENU, HUD, INVENTORY }

   UiModes currentMode = UiModes.HUD;

   void Update()
   {
      if(PlayerController.PlayerInstance == null)
         currentMode = UiModes.MENU;
      else
      {
         if(Input.GetKeyDown(KeyCode.Escape))
            currentMode = currentMode == UiModes.MENU ? UiModes.HUD : UiModes.MENU;
         if(currentMode != UiModes.MENU && Input.GetKeyDown(KeyCode.Tab))
            currentMode = currentMode == UiModes.INVENTORY ? UiModes.HUD : UiModes.INVENTORY;
      }
      switch(currentMode)
      {
         case UiModes.MENU:
            Menu.gameObject.SetActive(true);
            HUD.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(false);
            Menu.Start.text = "Restart";
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
      PlayerController.PlayerInstance.BlockShooting = currentMode != UiModes.HUD;
   }

}
