using UnityEngine;
using System.Collections;

public class attackPlayer : MonoBehaviour {
	
	public int damageGiven;
	public GameObject damageNumber;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Attacking here
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "player_1") 
		{
			other.gameObject.GetComponent<playerHealthManager>().damagePlayer(damageGiven);
			
			var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
			clone.GetComponent<floatingNumbers>().damageNumber = damageGiven;
			
//			var player = other.GetComponent<playerController>();
//			player.knockBackCount = player.knockBackLength;
//			
//			if(other.transform.position.x < transform.position.x)
//				player.knockFromRight = true;
//			else 
//				player.knockFromRight = false;
		}
	}
}
