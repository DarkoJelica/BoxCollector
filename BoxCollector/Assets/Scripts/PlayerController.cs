using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController {

   public float Sensitivity;
   public float MaxCamPitch;
   public BulletPool BulletPool;
   public float MinShotInterval;
   public int InventoryRows;
   public int InventoryColumns;
   public float PickupRange;
   [HideInInspector]
   public bool BlockShooting;

   public int Ammo
   {
      get
      {
         return ammo.Count;
      }
   }
   public int Boxes
   {
      get
      {
         return boxes.Count;
      }
   }
   public List<List<Collectible>> Inventory { get; private set; }
   public Collectible PickupObject { get; private set; }
   public Transform Viewpoint { get; private set; }

   public static PlayerController PlayerInstance;
   
   Transform bulletOrigin;
   float pitch = 0f;
   float yaw = 0f;
   Vector3 lastMouse;
   float lastShotTime;
   List<Collectible> ammo = new List<Collectible>();
   List<Collectible> boxes = new List<Collectible>();
   
	void OnEnable() {
      Viewpoint = transform.Find("Viewpoint");
      bulletOrigin = Viewpoint.transform.GetChild(0);
      yaw = transform.eulerAngles.y;
      lastMouse = Input.mousePosition;
      PlayerInstance = this;
      Inventory = new List<List<Collectible>>();
      int inventorySize = InventoryRows * InventoryColumns;
      for(int i = 0; i < inventorySize; ++i)
         Inventory.Add(new List<Collectible>());
	}

   void Update()
   {
      base.UpdateZoneDamage();
      UpdateInventory();
      Vector3 deltaMouse = Input.mousePosition - lastMouse;
      lastMouse = Input.mousePosition;
      yaw += deltaMouse.x * Sensitivity;
      yaw %= 360;
      pitch -= deltaMouse.y * Sensitivity;
      pitch = Mathf.Clamp(pitch, -MaxCamPitch, MaxCamPitch);
      transform.rotation = Quaternion.Euler(0, yaw, 0);
      Viewpoint.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
      if(Ammo > 0 && !BlockShooting && Input.GetMouseButtonDown(0) && Time.time - lastShotTime > MinShotInterval)
      {
         if(BulletPool.ShootBullet(bulletOrigin.position, bulletOrigin.forward))
            lastShotTime = Time.time;
         Destroy(ammo[ammo.Count - 1].gameObject);
      }
      Vector3 move = Vector3.zero;
      if(Input.GetKey(KeyCode.W))
         move.z += 1;
      if(Input.GetKey(KeyCode.S))
         move.z -= 1;
      if(Input.GetKey(KeyCode.A))
         move.x -= 1;
      if(Input.GetKey(KeyCode.D))
         move.x += 1;
      targetPosition = transform.position + transform.TransformDirection(move).normalized;
      jump = Input.GetKey(KeyCode.Space);
      run = Input.GetKey(KeyCode.LeftShift);
      PickupObject = null;
      RaycastHit hit;
      Physics.Raycast(Viewpoint.transform.position, Viewpoint.transform.forward, out hit);
      if(hit.transform != null && hit.distance < PickupRange)
         PickupObject = hit.transform.GetComponent<Collectible>();
      if(Input.GetKey(KeyCode.E))
         Pickup();
	}

   public void Pickup()
   {
      if(PickupObject == null)
         return;
      List<Collectible> slot = null;
      for(int i = 0; i < Inventory.Count; ++i)
      {
         if(slot == null && Inventory[i].Count == 0)
            slot = Inventory[i];
         if(Inventory[i].Count > 0 && Inventory[i][0].StackID == PickupObject.StackID && Inventory[i].Count < PickupObject.MaxStackSize)
         {
            slot = Inventory[i];
            break;
         }
      }
      if(slot == null)
      {
         Debug.Log("Couldn't pick up " + PickupObject.name + ": no slots available");
         return;
      }
      slot.Add(PickupObject);
      PickupObject.transform.parent = transform;
      PickupObject.transform.localPosition = Vector3.zero;
      PickupObject.gameObject.SetActive(false);
      if(PickupObject.Type == CollectibleTypes.AMMO)
         ammo.Add(PickupObject);
      else if(PickupObject.Type == CollectibleTypes.BOX)
         boxes.Add(PickupObject);
   }

   void UpdateInventory()
   {
      for(int i = 0; i < Inventory.Count; ++i)
         Inventory[i].RemoveAll(collectible => collectible == null || collectible.transform.parent != transform);
      ammo.RemoveAll(collectible => collectible == null || collectible.transform.parent != transform);
      boxes.RemoveAll(collectible => collectible == null || collectible.transform.parent != transform);
   }

}
