using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

   [HideInInspector]
   public int SlotIndex;
   public Image Background;
   public Image Icon;
   public Text Quantity;

   void OnGUI()
   {
      if(SlotIndex < 0 || SlotIndex >= PlayerController.PlayerInstance.Inventory.Count)
         return;
      List<Collectible> slot = PlayerController.PlayerInstance.Inventory[SlotIndex];
      if(slot.Count > 0)
      {
         Icon.sprite = slot[0].Icon;
         Icon.gameObject.SetActive(true);
         Quantity.text = slot.Count.ToString();
         Quantity.gameObject.SetActive(true);
      }
      else
      {
         Icon.gameObject.SetActive(false);
         Quantity.gameObject.SetActive(false);
      }
   }

   public void SetAnchor(Vector2 anchor)
   {
      Background.rectTransform.anchoredPosition = anchor;
      Icon.rectTransform.anchoredPosition = anchor;
      Quantity.rectTransform.anchoredPosition = anchor;
   }

   public void SetSize(Vector2 size)
   {
      Background.rectTransform.sizeDelta = size;
      Icon.rectTransform.sizeDelta = size;
      Quantity.rectTransform.sizeDelta = size;
   }

}
