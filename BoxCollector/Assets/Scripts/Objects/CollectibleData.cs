using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectibleData", order = 1)]
public class CollectibleData : ScriptableObject {
   public int MaxStackSize;
   public int StackID;
   public Sprite Icon;
   public CollectibleTypes Type;
   public bool DestroyOnDrop;
}
