using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour {

   public float Health;
   public float CurrentHealth { get; private set; }

   void OnEnable()
   {
      CurrentHealth = Health;
   }

   public void ReceiveDamage(float damage)
   {
      CurrentHealth -= damage;
      CurrentHealth = Mathf.Clamp(CurrentHealth, 0, Health);
      if(CurrentHealth < Mathf.Epsilon)
         Destroy(gameObject);
   }
	
}
