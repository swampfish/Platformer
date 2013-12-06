using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D ammo;				// Prefab of the ammo.
	public float speed = 20f;				// The speed the ammo will fire at.
	
	
	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.

	void Awake()
	{
		// Setting up the references.
		playerCtrl = transform.parent.GetComponent<PlayerControl>();
		Rigidbody2D bulletInstance = Instantiate(ammo, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(speed, 0);
	}
	
	
	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1"))
		{

			
			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// ... instantiate the ammo facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(ammo, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			}
			else
			{
				// Otherwise instantiate the ammo facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(ammo, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
			}
		}
	}
}
