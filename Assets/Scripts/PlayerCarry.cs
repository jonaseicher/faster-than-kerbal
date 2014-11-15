using UnityEngine;
using System.Collections.Generic;

public class PlayerCarry : MonoBehaviour
{

	public GameObject suckInObject;
	public float suckStep = 1f;
	public float forceReach = 10f;
	public float proportionalForce = 30f;
	public float differentialForce = 30f;	
	public int carryCapacity = 13;
	private Animator anim;				// Reference to the animator component.
	private DoneHashIDs hash;			// Reference to the HashIDs.
	
	
	
	private  HashSet<GameObject> pickups = new HashSet<GameObject> ();
	
	
		//Input field holders
	float h;
	float v;
	bool b;
	
	
	Shader highlightShader;
	Shader normalShader;

		
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (DoneTags.gameController).GetComponent<DoneHashIDs> ();
		
		// Set the weight of the shouting layer to 1.
		anim.SetLayerWeight (1, 1f);
		
		 highlightShader = Shader.Find( "Self-Illumin/Specular" );
	     normalShader = Shader.Find( "Bumped Specular" );
		//Debug.Log(highlightShader + "|" + normalShader);
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag.Equals ("PickUp") || other.tag.Equals ("PowerCube")) {
			if (pickups.Count < carryCapacity) {
				pickups.Add (other.gameObject);				
				
//				other.gameObject.GetComponentInChildren<Renderer>().material.SetColor ("_Color", Color.red);
//				other.gameObject.renderer.material.shader = highlightShader;
				
				//Debug.Log("ASDFDSFA");
				
				setShader(other.gameObject, highlightShader);				
//				other.light.intensity = 2.5f;				
			
				//Debug.Log ("ENTER: added to pickups " + other.gameObject.name);
			}
//			Debug.Log("ENTER: PickUp Object enterered trigger collider: " + other.gameObject.name );
		}
	}
	

	
	void OnTriggerStay (Collider other)
	{
		
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag.Equals ("PickUp") || other.tag.Equals ("PowerCube")) {
			if (!b) {
				leaveSelection(other.gameObject);
			}
		}
	}

	
	private void clearSelection()
	{
		foreach(GameObject selected in new HashSet<GameObject>(pickups))
		{
			leaveSelection(selected);
		}
	}
	
	private void leaveSelection(GameObject leaver)
	{
				setShader(leaver, normalShader);
				pickups.Remove(leaver);
				//Debug.Log ("EXIT: PickUp Object exits pickups" + leaver.name);
	}
	
	void FixedUpdate ()
	{
		
		InteractionManagement (b);
	}
	
	void Update ()
	{	
				// Cache the inputs.
		b = Input.GetKey (KeyCode.B);
		bool bUp = Input.GetKeyUp(KeyCode.B);
		
		
		if(bUp) { clearSelection(); }
		
		
		AudioManagement ();
	}
	
	void InteractionManagement (bool buttonPressed)
	{
		if (buttonPressed && pickups.Count > 0) {
			foreach (GameObject pickUp in pickups) {
				Vector3 objectPosition = pickUp.transform.position;
				Vector3 targetVector = suckInObject.transform.position - objectPosition;
//				if (targetVector.magnitude > forceReach) {
//					Debug.Log ("MoveTowards");
//					pickUp.transform.position = Vector3.MoveTowards (pickUp.transform.position, suckInObject.transform.position, suckStep * Time.deltaTime);
//				} else {
					pickUp.rigidbody.AddForce (targetVector * proportionalForce - pickUp.rigidbody.velocity * differentialForce, ForceMode.Acceleration);
//				}
			}
		}
	}
	

	
	void AudioManagement ()
	{
		
	}
	
		void setShader(GameObject go, Shader shader)
	{
		Renderer r = go.GetComponentInChildren<Renderer>();
		foreach (Material m in r.materials)
		{ 
			//Debug.Log("changing material: " + m.name);
//			m.SetColor("_Color", Color.red);
			m.shader = shader; 
		}
	}
}
