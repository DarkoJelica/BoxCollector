using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController {

   public float Sensitivity;
   public float MaxCamPitch;
   public BulletPool BulletPool;
   public float MinShotInterval;

   Camera playerCam;
   Transform bulletOrigin;
   float pitch = 0f;
   float yaw = 0f;
   Vector3 lastMouse;
   float lastShotTime;
   DamageReceiver playerHealth;
   
	void OnEnable() {
      playerCam = GetComponentInChildren<Camera>();
      bulletOrigin = playerCam.transform.GetChild(0);
      yaw = transform.eulerAngles.y;
      lastMouse = Input.mousePosition;
      playerHealth = GetComponent<DamageReceiver>();
	}

   void Update()
   {
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
      DamageZone.ApplyDamage(playerHealth);
	}

}
