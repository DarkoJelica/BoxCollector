using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

   public int PoolSize;
   public GameObject BulletPrefab;

   List<GameObject> pooledBullets = new List<GameObject>();
   List<GameObject> activeBullets = new List<GameObject>();
   
	void Start() {
      for(int i = 0; i < PoolSize; ++i)
      {
         GameObject bulletInstance = Instantiate(BulletPrefab);
         bulletInstance.SetActive(false);
         bulletInstance.transform.parent = transform;
         pooledBullets.Add(bulletInstance);
      }
	}

   void Update()
   {
      int bulletIndex = 0;
      while(bulletIndex < activeBullets.Count)
      {
         if(activeBullets[bulletIndex].activeInHierarchy)
         {
            bulletIndex++;
            continue;
         }
         pooledBullets.Add(activeBullets[bulletIndex]);
         activeBullets[bulletIndex].SetActive(false);
         activeBullets.RemoveAt(bulletIndex);
      }
   }

   public bool ShootBullet(Vector3 origin, Vector3 direction)
   {
      if(pooledBullets.Count == 0)
         return false;
      GameObject bullet = pooledBullets[0];
      pooledBullets.RemoveAt(0);
      activeBullets.Add(bullet);
      bullet.transform.position = origin;
      bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
      bullet.SetActive(true);
      return true;
   }

}
