 using UnityEngine;
using System.Collections;

public class NPC_movement : MonoBehaviour {
	
	// Speed
	public float moveSpeed;
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;

	// Animation purposes
	private Animator animator;

	// Body
	private Rigidbody2D myRigidbody;

	// Movement
	public bool movement;
	public float movementTime;
	private float movementCounter;
	public float waitTime;
	private float waitCounter;
	private int movementDirection;

	public Collider2D walkZone;
	private bool hasWalkZone;

	public bool canMove;

	private DialogueManager theDM;
	
	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		theDM = FindObjectOfType<DialogueManager>();

		waitCounter = waitTime;
		movementCounter = movementTime;
		
		ChooseDirection();

		if (walkZone != null)
		{
			minWalkPoint = walkZone.bounds.min;
			maxWalkPoint = walkZone.bounds.max;
			hasWalkZone = true;
		}

		canMove = true;
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if(!theDM.dialogueActive) 
		{
			canMove = true;
		}
		
		if (!canMove)
		{
			myRigidbody.velocity = Vector2.zero;
			return;
		}

		if (movement)
		{
			movementCounter -= Time.deltaTime;
			
			switch(movementDirection)
			{
			case 0:
				myRigidbody.velocity = new Vector2(0, moveSpeed);
					if (hasWalkZone && transform.position.y > maxWalkPoint.y)
					{
						movement = false;
						waitCounter = waitTime;
					}
				break;
							

			case 1: 
				myRigidbody.velocity = new Vector2(moveSpeed, 0);	
					if (hasWalkZone && transform.position.x > maxWalkPoint.x)
					{
						movement = false;
						waitCounter = waitTime;
					}
				break;

			case 2:
				myRigidbody.velocity = new Vector2(0, -moveSpeed);
					if (hasWalkZone && transform.position.y < minWalkPoint.y)
					{
						movement = false;
						waitCounter = waitTime;
					}
				break;

			case 3: 
				myRigidbody.velocity = new Vector2(-moveSpeed, 0);
					if (hasWalkZone && transform.position.x < maxWalkPoint.x)
					{
						movement = false;
						waitCounter = waitTime;
					}
				break;

			}

			if (movementCounter < 0)
			{
				movement = false;
				waitCounter = waitTime;
			}

		}
			else
			{
				waitCounter -= Time.deltaTime;

				myRigidbody.velocity = Vector2.zero;

				if (waitCounter < 0)
				{
					ChooseDirection();
				}

			}

	}

	public void ChooseDirection()
	{
		movementDirection = Random.Range (0,4);
		movement = true;
		movementCounter = movementTime;
	}
}

