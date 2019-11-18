using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour {

   public Collectible Collectible;
   public float SpawnTime;

   Collectible instance;
   bool spawning = false;
   
	void Start () {
      StartCoroutine(Spawn(0));
	}
	
	void Update () {
      PlayerController player = PlayerController.PlayerInstance;
      if(!spawning && (instance == null || (player != null && instance.transform.parent == player.transform)))
         StartCoroutine(Spawn(SpawnTime));
	}

   IEnumerator Spawn(float timer)
   {
      spawning = true;
      yield return new WaitForSeconds(timer);
      spawning = false;
      instance = Instantiate(Collectible, transform.position, transform.rotation);
      instance.transform.parent = transform;
   }
}
