using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

   public Vector3[] PatrolPoints;
   public float PointSwitchDistance;

   int currentPoint = 0;
   NavMeshAgent agent;

   void OnEnable()
   {
      agent = GetComponent<NavMeshAgent>();
   }

   void Update() {
      if(PatrolPoints.Length == 0)
         return;
      Vector3 deltaPos = transform.position - PatrolPoints[currentPoint];
      deltaPos.y = 0;
      if(deltaPos.sqrMagnitude < Mathf.Pow(PointSwitchDistance, 2))
         currentPoint++;
      if(currentPoint >= PatrolPoints.Length)
         currentPoint = 0;
      agent.destination = PatrolPoints[currentPoint];
	}
}
