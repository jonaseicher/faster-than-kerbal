#pragma strict

function OnMouseDown() {
	if (Input.GetMouseButton(0))
             Application.Quit();
}

function Update() {
	if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
}
