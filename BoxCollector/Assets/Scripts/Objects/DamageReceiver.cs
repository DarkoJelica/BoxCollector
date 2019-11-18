using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour {

   public float MaxHealth;
   public float CurrentHealth { get; private set; }

   void OnEnable()
   {
      CurrentHealth = MaxHealth;
   }

   public void ReceiveDamage(float damage)
   {
      CurrentHealth -= damage;
      CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
      if(CurrentHealth < Mathf.Epsilon)
         Destroy(gameObject);
   }
	
}
