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

   public int Ammo { get; private set; }
   public int Boxes { get; private set; }
   public List<List<Collectible>> Inventory { get; private set; }
   public Collectible PickupObject { get; private set; }

   public static PlayerController PlayerInstance;

   Camera playerCam;
   Transform bulletOrigin;
   float pitch = 0f;
   float yaw = 0f;
   Vector3 lastMouse;
   float lastShotTime;
   
	void OnEnable() {
      playerCam = GetComponentInChildren<Camera>();
      bulletOrigin = playerCam.transform.GetChild(0);
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
      Vector3 deltaMouse = Input.mousePosition - lastMouse;
      lastMouse = Input.mousePosition;
      yaw += deltaMouse.x * Sensitivity;
      yaw %= 360;
      pitch -= deltaMouse.y * Sensitivity;
      pitch = Mathf.Clamp(pitch, -MaxCamPitch, MaxCamPitch);
      transform.rotation = Quaternion.Euler(0, yaw, 0);
      playerCam.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
      if(Input.GetMouseButtonDown(0) && Time.time - lastShotTime > MinShotInterval)
      {
         if(BulletPool.ShootBullet(bulletOrigin.position, bulletOrigin.forward))
            lastShotTime = Time.time;
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
      DamageZone.ApplyDamage(Health);
      PickupObject = null;
      RaycastHit hit;
      Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit);
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
   }

}
