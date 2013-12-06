using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

	public int damage = 10;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2f);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.SendMessage ("Hurt", damage);
			Destroy(gameObject);
		}
	}
}
