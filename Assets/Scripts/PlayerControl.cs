using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {


	public int PlayerHP = 100;
	public float jumpForce = 250f;
	public float moveForce = 200f;
	public float maxSpeed = 2f;
	public Animator anim;

	private bool jump = false;
	private Transform groundCheck;
	private bool grounded = false;
	public bool facingRight = true;
	private bool invulnerable = false;
	private float invulStart;
	private bool PlayerControlEnabled = true;

	void Awake()
	{
		groundCheck = transform.Find ("groundCheck");
		anim = gameObject.GetComponent<Animator>();
	}

	// FixedUpdate is called once per "physics operation."
	void FixedUpdate()
	{
		if(invulnerable)
		{
			if(invulStart + 0.5f < Time.time)
			{
				invulnerable = false;
				PlayerControlEnabled = true;
			}
		}

		if(PlayerControlEnabled)
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

	void GetHurt(int damage)
	{
		if(!invulnerable)
		{
			PlayerHP -= damage;
			if (PlayerHP <= 0)
			{
				//TODO: die better
				Destroy(gameObject);
			}

			//control is diabled and player is made invulnerable for half a second
			PlayerControlEnabled = false;
			invulnerable = true;
			invulStart = Time.time;

			//toggling isKinematic removes all other forces applied to the player for a cleaner knockback
			rigidbody2D.isKinematic = true;		
			rigidbody2D.isKinematic = false;

			//knocks the player up and the opposite direction they are facing
			if(facingRight)
			{
				rigidbody2D.AddForce (new Vector2(-100, 150));
			}
			else
			{
				rigidbody2D.AddForce (new Vector2(100, 150));
			}
		}
	}
}
