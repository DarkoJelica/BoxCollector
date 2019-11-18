using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

   public EnemyController Enemy;
   public EnemyPath Path;
   public float RespawnTime;

   EnemyController instance;
   bool spawning = false;
   
	void Start () {
      StartCoroutine(Spawn(0));
	}
	
	void Update () {
      if(instance == null && !spawning)
         StartCoroutine(Spawn(RespawnTime));
	}

   IEnumerator Spawn(float timer)
   {
      spawning = true;
      yield return new WaitForSeconds(timer);
      spawning = false;
      instance = Instantiate(Enemy, transform.position, transform.rotation);
      instance.Path = Path;
      instance.transform.parent = transform;
   }
}
