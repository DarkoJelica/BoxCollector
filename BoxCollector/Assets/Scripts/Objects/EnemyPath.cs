using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PathData", order = 2)]
public class EnemyPath : ScriptableObject {
   public Vector3[] PatrolPoints;
}