using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour {

    public string text;
    public bool ready = false;
    public Color color;

    Vector3 movement;

    Text textObject;

	// Use this for initialization
	void Start () {
        movement = Vector3.up;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!ready)
        {
            return;
        }

        movement *= Time.deltaTime;
        gameObject.transform.Translate(movement);
	}

    public void Initialize(string text, Color color)
    {
        textObject = gameObject.GetComponent<Text>();
        textObject.fontSize = 14;
        textObject.text = text;
        textObject.color = color;
        ready = true;
    }
}
