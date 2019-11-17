using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ActorController {

   public Vector3[] PatrolPoints;
   public float PointSwitchDistance;

   int currentPoint = 0;
	
	void Update() {
      base.UpdateZoneDamage();
      if(PatrolPoints.Length == 0)
         return;
      Vector3 deltaPos = transform.position - PatrolPoints[currentPoint];
      deltaPos.y = 0;
      if(deltaPos.sqrMagnitude < Mathf.Pow(PointSwitchDistance, 2))
         currentPoint++;
      if(currentPoint >= PatrolPoints.Length)
         currentPoint = 0;
      targetPosition = PatrolPoints[currentPoint];
	}
}
