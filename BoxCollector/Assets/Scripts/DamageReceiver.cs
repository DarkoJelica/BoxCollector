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
      currentHealth = Mathf.Clamp(currentHealth, 0, Health);
      if(currentHealth < Mathf.Epsilon)
         Destroy(gameObject);
   }
	
}
