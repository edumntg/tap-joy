using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public bool isOnFloor;
    public bool started = false;
	public bool alive = true;
    public int scoreCount;
    public int score
    {
        get { return scoreCount; }
        set
        {
            scoreCount += value;
        }
    }
	// Use this for initialization
	void Start () {
        isOnFloor = false;
        scoreCount = 0;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isOnFloor = true;
        }
        if(other.gameObject.tag == "Obstacle")
        {
            //PLAYER IS DEAD!!!!!
            Scrolling environmentScrolling = other.GetComponentInParent<Scrolling>();
            if(environmentScrolling != null)
            {
                environmentScrolling.speed = Vector3.zero;
                environmentScrolling.scroll = false;
                Kill();
            }
        }
        if(other.gameObject.tag == "Valuable")
        {
            //coin taken
            score += 1;
            Destroy(other.gameObject);
        }
    }

    public Vector3 GetPosition() { return transform.position; }
    public bool CanJump() { return gameObject.transform.position.y == 0.5f; }
    public bool GetGameObject() { return gameObject; }

    public void Kill()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        if(anim != null && anim.GetBool("Alive") == true)
        {
            anim.Stop();
        }
		alive = false;
    }

    public void StartGame()
    {
        started = true;
        Text[] t = gameObject.transform.parent.parent.GetComponentsInChildren<Text>();
        foreach(Text tt in t)
        {
            if(tt.gameObject.name == "TapToStart")
            {
                tt.enabled = false;
                break;
            }
        }
    }
}
