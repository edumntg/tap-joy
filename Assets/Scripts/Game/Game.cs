using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public static Vector3 baseScrollingSpeed = new Vector3(8.0f, 0.0f, 0.0f);
	public static Vector3 baseScrollingDirection = new Vector3(-1.0f, 0.0f, 0.0f);
	public static Vector3 scrollingSpeedIncrease = new Vector3(1.0f, 0.0f, 0.0f);
	public static int speedIncreaseTime = 15; //ms

	
	public GameObject playerObject;
    public static Scrolling scrollingScript;

	void Start () {

		//get player gameObject
		foreach(Transform t in GetComponentsInChildren<Transform>())
		{
			if(t.name == "Player")
			{
				playerObject = t.gameObject;
				break;
			}
		}

		//get game scrolling script
		scrollingScript = GetComponentInChildren<Scrolling>();
	}
}
