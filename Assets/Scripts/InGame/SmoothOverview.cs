// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden

using UnityEngine;
using System.Collections;

public class SmoothOverview : MonoBehaviour
{
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 10.0f;
	// How much we 
	public float angle;
	// position damping
	public float positionDamping;
	
	void  LateUpdate ()
	{
		// Early out if we don't have a target
		if (!target)
			return;
		
		// Calculate the current rotation angles
		float wantedHeight = target.position.y + height;
		float currentHeight = transform.position.y;

		transform.position = Vector3.Lerp (transform.position, target.position+ Vector3.up*height, positionDamping*Time.time);
	}
}