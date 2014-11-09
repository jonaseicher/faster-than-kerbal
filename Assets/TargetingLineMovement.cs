using UnityEngine;
using System.Collections;

public class TargetingLineMovement : MonoBehaviour {

    public LineRenderer VerticalLine;
    public LineRenderer HorizontalLine;
    public float SpeedX; //6 
    public float SpeedY;  //6
    public float FiringTime; // = 3.0f;
    public float ReloadTime; // = 11.0f;
    public Transform targetTransform;
    public float MoveSpeed; //.1f

    private float x = 0, y = 0;
    private int top = -3, bottom = 3, left = -4, right = 2;
    private bool firing = false;
    private Vector2 leftBorder = new Vector2(0, 0);
    private float fireTimeStamp;

    
	void Awake () {
	    
	}
	
	void Update () {

        // Fire
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!firing)
            {
                StartFiring();               
            }
        }

        // 2 Modes, Targeting and Firing
        if (firing)
        {
            Fire();
        }
        else
        {
            MoveTarget();
            MoveTargetingLines();
        }
	}

    void MoveTarget()
    {
            targetTransform.Translate(Input.GetAxisRaw("Horizontal")*MoveSpeed, Input.GetAxisRaw("Vertical")*MoveSpeed, 0);       
    }

    void StartFiring() 
    {
        firing = true;
        VerticalLine.material.color = Color.red;
        HorizontalLine.material.color = Color.red;
        fireTimeStamp = Time.time;
    }
    void StopFiring() 
    {
        VerticalLine.material.color = Color.green;
        HorizontalLine.material.color = Color.yellow;
        firing = false;
    }


    void Fire()
    {
        //Debug.Log(Time.time - fireTimeStamp);
        if (Time.time - fireTimeStamp > FiringTime)
        {
            StopFiring();
        }

    }

    void MoveTargetingLines()
    {
        y = Mathf.PingPong(SpeedY * Time.time, bottom - top) + top;
        x = Mathf.PingPong(SpeedX * Time.time, right - left) + left;

        VerticalLine.SetPosition(0, new Vector2(x, top));
        VerticalLine.SetPosition(1, new Vector2(x, bottom));

        HorizontalLine.SetPosition(0, new Vector2(left, y));
        HorizontalLine.SetPosition(1, new Vector2(right, y));


        //transform.position = Vector3.Lerp(currentposition,wantedPosition)

        //Debug.Log("x=" + x + " y=" + y);
    }
}
