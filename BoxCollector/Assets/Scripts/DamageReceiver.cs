using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour {

   public float Health;

   float currentHealth;

   void OnEnable()
   {
      currentHealth = Health;
   }

   public void ReceiveDamage(float damage)
   {
      currentHealth -= damage;
      if(currentHealth < 0)
         Destroy(gameObject);
   }
	
}
