using UnityEngine;
using System.Collections;

public class MaintenanceButton : MonoBehaviour {

    private bool playerInRange = false;
    
    public AudioClip maintenanceButtonSound;

    public float startTime = 30.0f;
    public float timeAfterReset = 30.0f;
    private bool timeUp = false;
    
    public Light maintenanceLight;
    Color color0 = Color.green;
    Color color1 = Color.red;

    void Start()
    {
        maintenanceLight.color = color0;            // start with green light color
    }

	void Update () {                                // counts down from timeUp value
        startTime -= Time.deltaTime;
        Debug.Log(startTime);                        // display remaining time in debug window
        if (startTime < 0)
        {
            MaintenanceTimeUp();
        }
	
        if (playerInRange && Input.GetButtonUp("Use"))
        {
            Debug.Log("Maintenance activated.");
            MaintenanceActivated();
	}
}
    void OnTriggerEnter(Collider collider)          // checks if the player is in range
    {
        if(collider.tag == DoneTags.player && !collider.isTrigger)
        {
            Debug.Log("Player entered maintenance range.");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider collider)           // checks if the player is in range
    {
        if (collider.tag == DoneTags.player && !collider.isTrigger)
        {
            Debug.Log("Player left maintenance range.");
            playerInRange = false;
        }
    }

    void MaintenanceActivated()                     // resets time, plays sound, light goes green (if it was red before)
    {
        startTime = timeAfterReset;
        audio.PlayOneShot(maintenanceButtonSound);
        
        if (timeUp == true)
        { maintenanceLight.color = color0; }
    }

    void MaintenanceTimeUp()                        // if time is up, light goes red
    {
        maintenanceLight.color = color1;
        timeUp = true;
    }
}