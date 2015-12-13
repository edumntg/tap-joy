using UnityEngine;
using System.Collections;
using System;

public class Scrolling : MonoBehaviour {

    Vector3 speed = Game.baseScrollingSpeed;
	Vector3 direction = Game.baseScrollingDirection;

    public bool linkedToCamera = false;
    public bool isLooping = true;
    public bool scroll = false;

    Vector3 movement;
    int timeElapsed = 0;
    int lastIncreased = 0;
	
	// Update is called once per frame
	void Update () {

        if(!scroll)
        {
            return;
        }

        Transform[] childs = gameObject.GetComponentsInChildren<Transform>();
        movement = new Vector3(speed.x * direction.x, speed.y * direction.y, speed.z * direction.z);
        movement *= Time.deltaTime;
        foreach(Transform t in childs)
        {
            if (t.tag == "Floor" || t.tag == "Valuable" || t.tag == "MovableEffect")
            {
                t.Translate(movement);
            }
        }

        if(linkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        timeElapsed = Convert.ToInt32(Time.time);
        if(timeElapsed > 0 && timeElapsed % Game.speedIncreaseTime == 0 && timeElapsed > lastIncreased)
        {
            lastIncreased = timeElapsed;
            speed += Game.scrollingSpeedIncrease;
        }
	}

    public void Start() { scroll = true; }
    public void Stop() { scroll = false;}
    public Vector3 GetSpeed() { return speed; }
    public Vector3 GetDirection() { return direction; }
}
