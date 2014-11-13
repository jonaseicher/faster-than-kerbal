using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

void  OnMouseDown (){
	if (Input.GetMouseButton(0))
             Application.Quit();
}

void  Update (){
	if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
}

}