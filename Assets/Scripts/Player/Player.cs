using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
    public bool started = false;
	public bool alive = true;
    public bool isJumping = false;
    public int scoreCount;
    public Rigidbody body;
    public bool grounded = false;
    public int score
    {
        get { return scoreCount; }
        set
        {
            scoreCount = value;
            Text scoreText = gameObject.transform.parent.parent.FindChild("UI").GetComponentInChildren<Text>();
            if(scoreText != null)
            {
                scoreText.text = scoreCount.ToString();
            }
        }
    }

	void Start () {
        scoreCount = 0;
        body = gameObject.GetComponent<Rigidbody>();
        body.velocity = new Vector3(0, -100.0f, 0);
	}

    void FixedUpdate()
    {
        if(GetPosition().y < 0) //negative
        {
            Kill();
        }
    }

    public void Kill()
    {
		alive = false;
        Game.StopScrolling();
        Vector3 Vo = GetBody().velocity;
        GetBody().velocity = new Vector3(0, Vo.y + Physics.gravity.y * Time.deltaTime, 0);
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

        Time.timeScale = 1;
    }

    Object EffectObject;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Floor")
        {
            isJumping = false;
            grounded = true;
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
        }
        if (collider.gameObject.tag == "Valuable")
        {
            string itemName = collider.gameObject.name.Replace("Diamond(Clone)", " Diamond(Clone)").Replace("(Clone)", "");
            Item item = Game.items.GetItem(itemName);
            if (item != null)
            {
                score += item.GetValue();
            }

            //effect
            MagicEffect effect = Game.effects.GetEffect(item.GetAttribute("DeathEffect"));
            Vector3 position = collider.gameObject.transform.position;

            Destroy(collider.gameObject); //destroy the game object before sending the effect

            Game.SendMagicEffect(effect, position);         
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!Physics.Raycast(GetPosition(), Vector3.down, 10) && GetPosition().y < 0.55)
        {
            if (alive)
            {
                Kill();
            }
        }
    }

    public Vector3 GetPosition() { return gameObject.transform.position; }
    public bool CanJump() { return !isJumping && grounded; }
    public bool GetGameObject() { return gameObject; }
    public bool IsGrounded() { return grounded; }
    public Rigidbody GetBody() { return body; }
}
