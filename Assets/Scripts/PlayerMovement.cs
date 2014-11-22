using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	public enum Controls
	{
		simple,
		steering
	}
	
	public Controls controls = Controls.simple;
	public float turnSmoothing = 100f;	// A smoothing value for turning the player.
	public float moveSpeed = 5.5f;
	public float turnSpeed = 250f;
	public float speedDampTime = 0.1f;	// The damping for the speed parameter
	
	private Animator anim;				// Reference to the animator component.
	private DoneHashIDs hash;			// Reference to the HashIDs.
	
	
	
	private  HashSet<GameObject> pickups = new HashSet<GameObject> ();
	
	
		//Input field holders
	float h;
	float v;
    bool x;
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (DoneTags.gameController).GetComponent<DoneHashIDs> ();
		
		// Set the weight of the shouting layer to 1.
		anim.SetLayerWeight (1, 1f);
		
		
	}
	
	

	void FixedUpdate ()
	{
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		if (controls == Controls.simple) {
			MovementManagement (h, v);
		} else if (controls == Controls.steering) {
			MovementManagement2 (h, v);
		}
		
	}
	
	void Update ()
	{	
				// Cache the inputs.
		x = Input.GetKeyDown (KeyCode.X);
		
	
		
		if (x) {
			if (controls == Controls.simple) {
				controls = Controls.steering;
				Debug.Log("Setting controls to steering mode");
			} else if (controls == Controls.steering) {
				controls = Controls.simple;
				Debug.Log("Setting controls to simple mode");
			}
		}
		
		AudioManagement ();
	}
	
	
	void MovementManagement (float horizontal, float vertical)
	{
		// If there is some axis input...
		if (horizontal != 0f || vertical != 0f) {
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating (horizontal, vertical);
			anim.SetFloat (hash.speedFloat, moveSpeed, speedDampTime, Time.deltaTime);
		} else {
			// Otherwise set the speed parameter to 0.
			anim.SetFloat (hash.speedFloat, 0);
		}
	}
	
	void MovementManagement2 (float horizontal, float vertical)
	{

		// If there is some axis input...
		if (horizontal != 0f) {
			// ... set the players rotation and set the speed parameter to 5.5f.
			RotatingManually (horizontal);
		} 
		if (vertical != 0f) {
			anim.SetFloat (hash.speedFloat, moveSpeed * vertical, speedDampTime, Time.deltaTime);
		} else
			// Otherwise set the speed parameter to 0.
			anim.SetFloat (hash.speedFloat, 0);
	}
	
	// Horizontrale buttons drehen den char
	void RotatingManually (float horizontal)
	{
		Vector3 eulerAngleVelocity = new Vector3 (0, turnSpeed * horizontal, 0);
		Quaternion deltaRotation = Quaternion.Euler (eulerAngleVelocity * Time.deltaTime);
		rigidbody.MoveRotation (rigidbody.rotation * deltaRotation);
	}
	
	// horizontrale buttons bewegen den char in die richtung
	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
//		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation (targetRotation);
	}
	
	void AudioManagement ()
	{
		// If the player is currently in the run state...
		if (anim.GetCurrentAnimatorStateInfo (0).nameHash == hash.locomotionState) {
			// ... and if the footsteps are not playing...
			if (!audio.isPlaying)
				// ... play them.
				audio.Play ();
		} else
			// Otherwise stop the footsteps.
			audio.Stop ();
	}
	
	
}
