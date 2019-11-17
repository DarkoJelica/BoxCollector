using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

   public InventorySlot SlotPrefab;
   [Range(0f, 0.25f)]
   [Tooltip("Portion of inventory space dedicated to spacing between slots and edge of inventory background")]
   public float NormalizedMargin;
   [Range(0f, 0.25f)]
   [Tooltip("Portion of inventory slots space dedicated to spacing between slots")]
   public float NormalizedSpacing;

   List<InventorySlot> slots;
   Image background;
   bool initialized = false;

   void OnGUI()
   {
      if(!initialized)
         Initialize();
   }

   void Initialize()
   {
      PlayerController player = PlayerController.PlayerInstance;
      if(player == null)
         return;
      background = GetComponent<Image>();
      Vector2 bkgSize = background.rectTransform.sizeDelta;
      Vector2 slotsSpace;
      slotsSpace.x = bkgSize.x * (1 - NormalizedMargin);
      slotsSpace.y = bkgSize.y * (1 - NormalizedMargin);
      if(slotsSpace.x / player.InventoryColumns > slotsSpace.y / player.InventoryRows)
         slotsSpace.x = bkgSize.x * slotsSpace.y / bkgSize.y;
      else
         slotsSpace.y = bkgSize.y * slotsSpace.x / bkgSize.x;
      float spacing = player.InventoryColumns < player.InventoryRows ? slotsSpace.y * NormalizedSpacing / player.InventoryRows : slotsSpace.x * NormalizedSpacing / player.InventoryColumns;
      Vector2 slotSize = slotsSpace;
      if(player.InventoryColumns > 1)
         slotSize.x = slotsSpace.x / player.InventoryColumns - spacing * ((float)(player.InventoryColumns - 1) / player.InventoryColumns);
      if(player.InventoryRows > 1)
         slotSize.y = slotsSpace.y / player.InventoryRows - spacing * ((float)(player.InventoryRows - 1) / player.InventoryRows);
      if(slotSize.x < slotSize.y)
         slotSize.y = slotSize.x;
      else
         slotSize.x = slotSize.y;
      Vector2 slotOffset = slotSize + new Vector2(player.InventoryColumns > 1 ? spacing : 0, player.InventoryRows > 1 ? spacing : 0);
      slotsSpace.x = Mathf.Clamp(slotsSpace.x, 0, slotOffset.x * player.InventoryColumns + slotSize.x - slotOffset.x);
      slotsSpace.y = Mathf.Clamp(slotsSpace.y, 0, slotOffset.y * player.InventoryRows + slotSize.y - slotOffset.y);
      Vector2 firstSlotAnchor;
      firstSlotAnchor.x = (slotSize.x - slotsSpace.x) / 2;
      firstSlotAnchor.y = (slotSize.y - slotsSpace.y) / 2;
      slots = new List<InventorySlot>();
      for(int i = 0; i < PlayerController.PlayerInstance.Inventory.Count; ++i)
      {
         InventorySlot slot = Instantiate(SlotPrefab);
         slot.SlotIndex = i;
         int row = Mathf.FloorToInt(i / PlayerController.PlayerInstance.InventoryColumns);
         int column = i - row * PlayerController.PlayerInstance.InventoryColumns;
         slot.SetAnchor(new Vector2(firstSlotAnchor.x + slotOffset.x * column, -(firstSlotAnchor.y + slotOffset.y * row)));
         slot.SetSize(slotSize);
         slot.transform.SetParent(transform);
         slots.Add(slot);
      }
      initialized = true;
   }

}
