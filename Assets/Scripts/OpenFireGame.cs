using UnityEngine;
using System.Collections;

public class OpenFireGame : MonoBehaviour {

	public Camera firecamera;

	void Start () {
	
		firecamera.enabled = false;

	}
	
	void Update () {
	
//	ontriggerenter
//	am Spieler: iscollider.gameobject.tag
//	collider.gameobject.getcomponent[skript]
//	in update-funktion enablen/disablen

		if (Input.GetKeyUp(KeyCode.E)) {
			firecamera.enabled = !firecamera.enabled;

	}
}
}