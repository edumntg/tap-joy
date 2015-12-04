using UnityEngine;
using System.Collections;
using System;

public class Jump : MonoBehaviour
{
	
	Vector3 jumpForce = new Vector3(0.0f, 18000.0f, 0.0f);
	Vector3 jumpDirection = new Vector3(0.0f, 1.0f, 0.0f);
	
	public bool isJumping = false;
	Player player;
	
	
	void Start()
	{
		player = GetComponent<Player>();
	}
	
	void Update()
	{
		if(!player.alive)
		{
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.W) && !isJumping)
		{
			GetComponent<Rigidbody>().AddForce(Vector3.Scale(jumpForce, jumpDirection));
			isJumping = true;
			player.StartGame();
			player.score += 1;
		}
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Floor")
		{
			isJumping = false;
		}        
	}
}