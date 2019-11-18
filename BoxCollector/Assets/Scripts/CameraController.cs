using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	void LateUpdate() {
      PlayerController player = PlayerController.PlayerInstance;
      if(player != null)
      {
         transform.position = player.Viewpoint.position;
         transform.rotation = player.Viewpoint.rotation;
      }
	}
}
