using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

   public InventorySlot SlotPrefab;
   public Image DraggedCollectible;
   [Range(0f, 0.25f)]
   [Tooltip("Portion of inventory space dedicated to spacing between slots and edge of inventory background")]
   public float NormalizedMargin;
   [Range(0f, 0.25f)]
   [Tooltip("Portion of inventory slots space dedicated to spacing between slots")]
   public float NormalizedSpacing;

   List<InventorySlot> slots;
   int draggedSlotIndex = -1;
   int draggedSlotQuantity = -1;
   Image background;
   Vector2 bkgSize;
   bool initialized = false;

   void OnGUI()
   {
      if(!initialized)
         Initialize();
      if(draggedSlotIndex >= 0 && draggedSlotQuantity > 0)
      {
         DraggedCollectible.rectTransform.position = Input.mousePosition;
         DraggedCollectible.sprite = PlayerController.PlayerInstance.Inventory[draggedSlotIndex][0].Icon;
         DraggedCollectible.gameObject.SetActive(true);
      }
      else
         DraggedCollectible.gameObject.SetActive(false);
   }

   void Update()
   {
      if(!initialized)
         return;
      PlayerController player = PlayerController.PlayerInstance;
      if(player == null)
         return;
      if(draggedSlotIndex < 0 && Input.GetMouseButtonDown(0))
      {
         draggedSlotIndex = GetMouseSlot();
         if(draggedSlotIndex >= 0)
            draggedSlotQuantity = player.Inventory[draggedSlotIndex].Count;
      }
      else if(draggedSlotIndex < 0 && Input.GetMouseButtonDown(1))
      {
         draggedSlotIndex = GetMouseSlot();
         if(draggedSlotIndex >= 0)
            draggedSlotQuantity = Mathf.CeilToInt(PlayerController.PlayerInstance.Inventory[draggedSlotIndex].Count / 2f);
      }
      if(draggedSlotIndex >= 0 && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)))
      {
         int targetSlot = GetMouseSlot();
         if(targetSlot >= 0)
            player.SwapSlots(draggedSlotIndex, draggedSlotQuantity, targetSlot);
         else
         {
            Vector3 relativePos = background.rectTransform.InverseTransformPoint(Input.mousePosition);
            if(Mathf.Abs(relativePos.x) > bkgSize.x / 2 || Mathf.Abs(relativePos.y) > bkgSize.y / 2)
               player.Drop(draggedSlotIndex, draggedSlotQuantity);
         }
         draggedSlotIndex = -1;
         draggedSlotQuantity = -1;
      }
   }

   int GetMouseSlot()
   {
      for(int i = 0; i < slots.Count; ++i)
      {
         Vector3 relativePos = slots[i].Background.rectTransform.InverseTransformPoint(Input.mousePosition);
         if(Mathf.Abs(relativePos.x) < slots[i].Background.rectTransform.sizeDelta.x / 2 && Mathf.Abs(relativePos.y) < slots[i].Background.rectTransform.sizeDelta.y / 2)
            return i;
      }
      return -1;
   }

   void Initialize()
   {
      PlayerController player = PlayerController.PlayerInstance;
      if(player == null)
         return;
      background = GetComponent<Image>();
      bkgSize = background.rectTransform.sizeDelta;
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
      DraggedCollectible.transform.SetAsLastSibling();
      initialized = true;
   }

}
