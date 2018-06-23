using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	// The speed of the enemy
	public float speed = 1.0f;
	
	// Body
	private Rigidbody2D myRigidbody;
	
	// Animation purposes
	private Animator animator;

	private Vector3 Enemy;
	public Vector2 EnemyDirection;

	public bool canMove;

	// Line of sight variable
	private int Wall;

	// Target
	public Transform player;

	// Distance
	private float distance;

	// Attack time
	private bool stun;
	private float stunned;

	// Reloading
	public float waitToReload;
	private bool reloading;
	private GameObject thePlayer;

	
	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
		Wall = 1 << 8;
		stun = false;

		canMove = true;
	}

	void Update () 
	{

		if(!canMove)
		{
			myRigidbody.velocity = Vector2.zero;
			return;
		} 

		if (stunned > 0)
		{
			stunned -= Time.deltaTime;
		}
		else 
		{
			stun = false;	
		}

		// Retrieve axis information
		if (distance < 10 && !stun)
		{
			var horizontal = Enemy.x - transform.position.x;
			var vertical = Enemy.y - transform.position.y;
			
			Enemy = GameObject.Find ("player_1").transform.position;

			// Line of sight following the player
			EnemyDirection = new Vector2 (horizontal, vertical);
	
			if (!Physics2D.Raycast (transform.position, EnemyDirection, 1, Wall)) 
			{
				GetComponent<Rigidbody2D>().AddForce(EnemyDirection.normalized * speed);
			}
     	
			// Directional control
			if (vertical > 1)
			{
				animator.SetInteger("Direction", 2);
				animator.SetFloat("Speed", 1.0f);
			}
			else if (vertical < -1)
			{
				animator.SetInteger("Direction", 0);
				animator.SetFloat("Speed", 1.0f);
			}
			else if (horizontal < -1)
			{
				animator.SetInteger("Direction", 1);
				animator.SetFloat("Speed", 1.0f);
			}
			else if (horizontal > 1)
			{
				animator.SetInteger("Direction", 3);
				animator.SetFloat("Speed", 1.0f);
			}
			else
			{
				animator.SetFloat("Speed", 0);
			}
	
	
			//if(reloading)
			//{
			//	waitToReload -= Time.deltaTime;
			//	if (waitToReload < 0)
			//	{
			//		Application.LoadLevel(Application.loadedLevel);
			//		thePlayer.SetActive(true);
			//	}
			//}
		}
	} 

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "player_1")
			{
				stun = true;
				stunned = 1;
				
				//GetComponent<Rigidbody2D>().AddForce(EnemyDirection * speed);

 				// Destroy (other.gameObject);
				//other.gameObject.SetActive(false);
				//reloading = true;
				// thePlayer = other.gameObject;
			} 
	}
}