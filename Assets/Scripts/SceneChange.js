#pragma strict

function OnMouseDown() {				// using Update function for this does NOT work!
	if (Input.GetMouseButton(0))
			Application.LoadLevel("Recruitment-Zone");
}