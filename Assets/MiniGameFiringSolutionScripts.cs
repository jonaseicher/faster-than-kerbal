using UnityEngine;
using System.Collections;

public class MiniGameFiringSolutionScripts : MonoBehaviour {


    public Camera MiniGameCamera;

    private bool inUseRange = false;
	
	void Awake () {

        //sphereCollider = GetComponent<SphereCollider>();
	}


    void OnTriggerEnter()
    {
        inUseRange = true;
    }

    void OnTriggerExit()
    {
        inUseRange = false;
    }

    void OnTriggerStay()
    {

    }

	void Update () {
        if (Input.GetButtonDown("Use"))
        {
            MiniGameCamera.enabled = !MiniGameCamera.enabled;

        }	
	}
}
