using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float jumpForce = 250f;
	public float moveForce = 200f;
	public float maxSpeed = 2f;

	private bool jump = false;
	private Transform groundCheck;
	private bool grounded = false;

	void Awake()
	{
		groundCheck = transform.Find ("groundCheck");
	}

	void FixedUpdate()
	{
		float inputX = Input.GetAxis("Horizontal");

		rigidbody2D.AddForce(Vector2.right * inputX * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
		{
			rigidbody2D.velocity = new Vector2(Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		if(jump)
		{
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}
}
