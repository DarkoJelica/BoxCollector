using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleTypes { BOX, AMMO, NONE }

public class Collectible : MonoBehaviour {

   public int MaxStackSize;
   public int StackID;
   public Sprite Icon;
   public CollectibleTypes Type;

   public static int BoxCount { get; private set; }

   void OnEnable()
   {
      if(Type == CollectibleTypes.BOX)
         BoxCount++;
   }

   void OnDestroy()
   {
      if(Type == CollectibleTypes.BOX)
         BoxCount--;
   }

}
