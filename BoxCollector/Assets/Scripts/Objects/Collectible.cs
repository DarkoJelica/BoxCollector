using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleTypes { BOX, AMMO, NONE }

public class Collectible : MonoBehaviour {

   public CollectibleData Data;

   public static int BoxCount { get; private set; }

   void OnEnable()
   {
      if(Data.Type == CollectibleTypes.BOX)
         BoxCount++;
   }

   void OnDestroy()
   {
      if(Data.Type == CollectibleTypes.BOX)
         BoxCount--;
   }

}
