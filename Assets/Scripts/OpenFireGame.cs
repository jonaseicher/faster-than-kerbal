using UnityEngine;
using System.Collections;

public class OpenFireGame : MonoBehaviour
{

    public Camera firecamera;
    private bool inRange = false;
    private bool inMiniGame = false;
    private TargetingLineMovement targetScript;
    public GameObject GameController;
 
    GameObject player;

    void Awake()
    {
        targetScript = gameObject.GetComponentInChildren<TargetingLineMovement>();
    }


    void Update()
    {

        //	ontriggerenter
        //	am Spieler: iscollider.gameobject.tag
        //	collider.gameobject.getcomponent[skript]
        //	in update-funktion enablen/disablen

        if (inRange && Input.GetButtonUp("Use"))
        {
            Debug.Log("Button Pressed and inRange.");
            
            if (inMiniGame)
            { EndMiniGame(); }
            else { BeginMiniGame(); }            
            
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == DoneTags.player && !collider.isTrigger)
        {
            Debug.Log("Player entered");
            inRange = true;
            player = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == DoneTags.player && !collider.isTrigger)
        {
            Debug.Log("Player exit");
            inRange = false;
            if (inMiniGame)
            { EndMiniGame(); }
            player = null;
        }
    }

    void BeginMiniGame()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        GameController.GetComponent<CameraSwitch>().enabled = false;
        targetScript.enabled = true;
        firecamera.enabled = true;
        inMiniGame = true;
        // enable minigame
    }

    void EndMiniGame()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        GameController.GetComponent<CameraSwitch>().enabled = true;
        firecamera.enabled = false;
        inMiniGame = false;
        targetScript.enabled = false;
        // disable minigame
    }


}