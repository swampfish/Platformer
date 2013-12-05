using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public int Damage;

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
}
