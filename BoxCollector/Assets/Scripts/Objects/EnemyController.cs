using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

   public EnemyPath Path;
   public float PointSwitchDistance;
   public static int EnemyCount { get; private set; }

   int currentPoint = 0;
   NavMeshAgent agent;

   void OnEnable()
   {
      agent = GetComponent<NavMeshAgent>();
      EnemyCount++;
   }

   void Update() {
      if(Path == null || Path.PatrolPoints.Length == 0)
      {
         agent.destination = transform.position;
         return;
      }
      Vector3 deltaPos = transform.position - Path.PatrolPoints[currentPoint];
      deltaPos.y = 0;
      if(deltaPos.sqrMagnitude < Mathf.Pow(PointSwitchDistance, 2))
         currentPoint++;
      if(currentPoint >= Path.PatrolPoints.Length)
         currentPoint = 0;
      agent.destination = Path.PatrolPoints[currentPoint];
	}

   void OnDestroy()
   {
      EnemyCount--;
   }
}
