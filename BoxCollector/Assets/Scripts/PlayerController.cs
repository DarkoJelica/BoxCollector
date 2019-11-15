using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

   public float Sensitivity;
   public float Speed;
   public float JumpSpeed;
   public float RunSpeedMultiplier;
   public float MaxCamPitch;
   public float MaxFloorNormal;
   public BulletPool BulletPool;
   public float MinShotInterval;

   Camera playerCam;
   Transform bulletOrigin;
   float pitch = 0f;
   float yaw = 0f;
   Vector3 lastMouse;
   Rigidbody playerBody;
   float lastShotTime;
   
	void Start () {
      playerCam = GetComponentInChildren<Camera>();
      bulletOrigin = playerCam.transform.GetChild(0);
      yaw = transform.eulerAngles.y;
      lastMouse = Input.mousePosition;
      playerBody = GetComponent<Rigidbody>();
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
	}

   void OnCollisionStay(Collision collision)
   {
      bool grounded = false;
      for(int i = 0; i < collision.contacts.Length; ++i)
      {
         if(Vector3.Angle(Vector3.up, collision.contacts[i].normal) <= MaxFloorNormal)
         {
            grounded = true;
            break;
         }
      }
      if(!grounded)
         return;
      float zSpeed = Input.GetKey(KeyCode.W) ? 1 : 0;
      zSpeed += Input.GetKey(KeyCode.S) ? -1 : 0;
      float xSpeed = Input.GetKey(KeyCode.A) ? -1 : 0;
      xSpeed += Input.GetKey(KeyCode.D) ? 1 : 0;
      if(Input.GetKey(KeyCode.LeftShift))
      {
         xSpeed *= RunSpeedMultiplier;
         zSpeed *= RunSpeedMultiplier;
      }
      float ySpeed = 0f;
      if(Input.GetKey(KeyCode.Space))
         ySpeed = JumpSpeed;
      playerBody.velocity = transform.TransformDirection(new Vector3(xSpeed, ySpeed, zSpeed));
   }

}
