using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public int Damage;
	public int HitPoints = 100;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void HurtPlayer()
	{
		GameObject player = GameObject.Find ("Player");
		player.SendMessage("GetHurt", Damage);
	}

	void Hurt(int damage)
	{
		HitPoints -= damage;
		if(HitPoints <= 0)
		{
			Destroy(gameObject);
		}
	}
}
