using UnityEngine;
using System.Collections;

public class attackEnemy : MonoBehaviour {

	public int damageGiven;
	public GameObject damageBurst;
	public Transform hitPoint;

	// Attack time
	private bool stun;
	private float stunned;

	//public GameObject damageNumber;
	
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
		if (other.gameObject.tag == "Enemy") 
		{
			// Destroy(other.gameObject);
			other.gameObject.GetComponent<EnemyHealthManager>().attackEnemy(damageGiven);
			Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);

			stun = true;
			stunned = 1;
			
			//var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
			//clone.GetComponent<floatingNumbers>().damageNumber = damageGiven;
		}
	}

}
