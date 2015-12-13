using UnityEngine;
using System.Collections;
using System;

public class Jump : MonoBehaviour
{
	
	Vector3 jumpForce = new Vector3(0.0f, 17500.0f, 0.0f);
	Vector3 jumpDirection = new Vector3(0.0f, 1.0f, 0.0f);
	Player player;
	
	
	void Start()
	{
        player = gameObject.GetComponent<Player>();
    }
	
	void Update()
	{
		if(!player.alive)
		{
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.W) && player.CanJump())
		{
			player.isJumping = true;
            player.grounded = false;
            gameObject.GetComponent<Animator>().SetBool("Jump", true);
            player.GetBody().velocity = Vector3.zero;
            player.GetBody().AddForce(Vector3.Scale(jumpForce, jumpDirection));
			player.StartGame();
			player.score += 1;
            //Game.SendAnimatedText("Hello", Color.white, player.GetPosition());

		}
	}

    public void AddJumpForce(Vector3 add) { jumpForce += add; }
}