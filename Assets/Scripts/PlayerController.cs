using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float jumpForce = 250f;
	public float moveForce = 200f;
	public float maxSpeed = 2f;
	public Animator anim;

	private bool jump = false;
	private Transform groundCheck;
	private bool grounded = false;
	private bool facingRight = true;

	void Awake()
	{
		groundCheck = transform.Find ("groundCheck");
		anim = gameObject.GetComponent<Animator>();
	}

	// FixedUpdate is called once per "physics operation."
	void FixedUpdate()
	{
		float inputX = Input.GetAxis("Horizontal");

		rigidbody2D.AddForce(Vector2.right * inputX * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
		{
			rigidbody2D.velocity = new Vector2(Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		if(inputX > 0 && !facingRight)
		{
			Flip();
		}
		else if(inputX < 0 && facingRight)
		{
			Flip();
		}



		if(jump)
		{
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

		anim.SetFloat("speed", Mathf.Abs (rigidbody2D.velocity.x));
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		//Apparently putting this in FixedUpdate is bad because if you skip a frame you could end up jumping twice.
		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
