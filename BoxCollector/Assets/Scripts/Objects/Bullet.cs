using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

   public float Speed;
   public float Range;
   public float Damage;

   Vector3 startPos;

   void OnEnable()
   {
      GetComponent<Rigidbody>().velocity = transform.forward * Speed;
      startPos = transform.position;
   }

   void Update()
   {
      if((transform.position - startPos).sqrMagnitude > Mathf.Pow(Range, 2))
         gameObject.SetActive(false);
   }

   void OnTriggerEnter(Collider other)
   {
      gameObject.SetActive(false);
      DamageReceiver damageReceiver = other.gameObject.GetComponent<DamageReceiver>();
      if(damageReceiver != null)
         damageReceiver.ReceiveDamage(Damage);
   }

}
