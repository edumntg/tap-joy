using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public static Vector3 baseScrollingSpeed = new Vector3(6.0f, 0.0f, 0.0f);
	public static Vector3 baseScrollingDirection = new Vector3(-1.0f, 0.0f, 0.0f);
	public static Vector3 scrollingSpeedIncrease = new Vector3(0.5f, 0.0f, 0.0f);
	public static int speedIncreaseTime = 15; //ms

	
	public GameObject playerObject;
	public Scrolling scrollingScript;

	// Use this for initialization
	void Start () {

		//get player gameObject
		foreach(GameObject obj in GetComponentsInChildren<GameObject>())
		{
			if(obj.name == "PlayerObject")
			{
				playerObject = obj;
				break;
			}
		}

		//get game scrolling script
		scrollingScript = GetComponentInChildren<Scrolling>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
