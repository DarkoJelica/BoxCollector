using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {

   public float DamagePerSecond;
   public float Size;

   Light zoneLight;
   static List<DamageZone> zones;

   void OnEnable()
   {
      zoneLight = GetComponentInChildren<Light>();
      if(zones == null)
         zones = new List<DamageZone>();
      zones.Add(this);
   }

   void Update()
   {
      zoneLight.cookieSize = Size;
      zones.RemoveAll(item => item == null);
   }

   public static void ApplyDamage(DamageReceiver receiver)
   {
      if(zones == null)
         return;
      for(int i = 0; i < zones.Count; ++i)
      {
         if(zones[i] == null)
            continue;
         Vector3 deltaPos = receiver.transform.position - zones[i].transform.position;
         if(Mathf.Abs(deltaPos.x) > zones[i].Size / 2)
            continue;
         if(Mathf.Abs(deltaPos.z) > zones[i].Size / 2)
            continue;
         receiver.ReceiveDamage(zones[i].DamagePerSecond * Time.deltaTime);
      }
   }

}
