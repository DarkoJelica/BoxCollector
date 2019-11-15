using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

   public float Speed;
   public float RunSpeedMultiplier;
   public float JumpSpeed;
   public float MaxFloorNormal;
   public float MoveThreshold;

   protected Vector3 targetPosition;
   protected bool run;
   protected bool jump;

   Rigidbody actorBody;

   void OnCollisionStay(Collision collision)
   {
      if(actorBody == null)
         actorBody = GetComponent<Rigidbody>();
      bool grounded = false;
      for(int i = 0; i < collision.contacts.Length; ++i)
      {
         if(Vector3.Angle(Vector3.up, collision.contacts[i].normal) <= MaxFloorNormal)
         {
            grounded = true;
            break;
         }
      }
      if(!grounded)
         return;
      Vector3 speed = targetPosition - transform.position;
      speed.y = 0;
      if(speed.sqrMagnitude > Mathf.Pow(MoveThreshold, 2))
         speed = speed.normalized * Speed;
      if(run)
         speed *= RunSpeedMultiplier;
      if(jump)
         speed.y = JumpSpeed;
      actorBody.velocity = speed;
   }
}
