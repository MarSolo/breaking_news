using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Speed
	public float moveSpeed;

	// Animation purposes
	private Animator animator;

	// Body
	private Rigidbody2D myRigidbody;

	// Movement
	private bool moving;
	public float timeBetweenMovement;
	private float timeBetweenMovementCounter;
	public float timeToMove;
	private float timeToMoveCounter;
	private Vector3 movementDirection;
	
	public bool canMove;
	
	// Attacking
	public int damageGiven;

	// Reloading Game
	public float waitToReload;
	private bool reloading;
	// private GameObject WhiteHouse;


	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		
		// timeBetweenMovementCounter = timeBetweenMovement;
		// timeToMoveCounter = timeToMove;

		timeBetweenMovementCounter = Random.Range(timeBetweenMovement * 0.75f, timeBetweenMovement * 1.25f);
		timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMovement * 1.25f);

		canMove = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!canMove)
		{
			myRigidbody.velocity = Vector2.zero;
			return;
		}
		
		if(moving)
		{
			timeToMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = movementDirection;

			if(timeToMoveCounter < 0f)
			{
					moving = false;
					// timeBetweenMovementCounter = timeBetweenMovement;
					timeBetweenMovementCounter = Random.Range(timeBetweenMovement * 0.75f, timeBetweenMovement * 1.25f);
			}
		}
		else
		{
			timeBetweenMovementCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;

			if(timeBetweenMovementCounter < 0f)
			{
				moving = true;
				// timeToMoveCounter = timeToMove;
				timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMovement * 1.25f);


				movementDirection = new Vector3(Random.Range(-1f, 1f)
					* moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
			}
		}
			
		if(reloading)
		{
			waitToReload -= Time.deltaTime;
			if (waitToReload < 0)
			{
				Application.LoadLevel(Application.loadedLevel); 
				// WhiteHouse.SetActive(true);
			}
		}
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name == "player_1")
		{
			other.gameObject.GetComponent<playerHealthManager>().damagePlayer(damageGiven);
			reloading = false;

			// Player = other.gameObject;
		}
	}
}
