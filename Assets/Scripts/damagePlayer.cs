using UnityEngine;
using System.Collections;

public class damagePlayer : MonoBehaviour {
	
	public int damageGiven;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "player_1")
		{
			other.gameObject.GetComponent<playerHealthManager>().damagePlayer(damageGiven);
		}
	}
}
