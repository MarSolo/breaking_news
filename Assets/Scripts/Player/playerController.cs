using UnityEngine;
using System.Collections;
using UnityEngine.UI;   // Allows us to use UI.

public class playerController : MonoBehaviour {
	
	// The speed of the player
	public float moveSpeed;
	private float currentMoveSpeed;
	// public float diagonalMoveModifier;
	
	// Animation purposes
	private Animator animator;
	
	// Body
	private Rigidbody2D myRigidbody;
	
	// Movement purposes
	private bool movement;
	public bool canMove;

	// Last movement of player before change occurs
	public Vector2 lastMovement;
	private Vector2 moveInput;

	// Attacking
	private bool attack;
	public float attackTime;
	private float attackTimeCounter;

	// Camera Flash
	private bool flash;
	public float flashTime;
	private float flashTimeCounter;

	// Touch Controls
	private Vector2 touchOrigin = -Vector2.one;	

	// loading Areas
	private static bool playerExists;
	public string startPoint;

	private SFXManager sfxMan;
	
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();	
		sfxMan = FindObjectOfType<SFXManager>();

		if(!playerExists)
		{
			playerExists = true;
			DontDestroyOnLoad(transform.gameObject);
		} 
		else 
		{
			Destroy(gameObject);
		}

		canMove = true;

		lastMovement = new Vector2(0, -1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		int horizontal = 0;     // Used to store the horizontal move direction.
        int vertical = 0;       // Used to store the vertical move direction.
		
		// Check if we are running either in the Unity editor or in a standalone build.
            #if UNITY_STANDALONE || UNITY_WEBPLAYER
            
            // Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
            
            // Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
            vertical = (int) (Input.GetAxisRaw ("Vertical"));

			// Check if moving horizontally, if so set vertical to zero.
			if(horizontal != 0)
			{
				vertical = 0;
			}	

		// Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
            #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            
            // Check if Input has registered more than zero touches
            if (Input.touchCount > 0)
            {
                // Store the first touch detected.
                Touch myTouch = Input.touches[0];
                
                // Check if the phase of that touch equals Began
                if (myTouch.phase == TouchPhase.Began)
                {
                    //If so, set touchOrigin to the position of that touch
                    touchOrigin = myTouch.position;
                }
                
                // If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    // Set touchEnd to equal the position of this touch
                    Vector2 touchEnd = myTouch.position;
                    
                    // Calculate the difference between the beginning and end of the touch on the x axis.
                    float x = touchEnd.x - touchOrigin.x;
                    
                    // Calculate the difference between the beginning and end of the touch on the y axis.
                    float y = touchEnd.y - touchOrigin.y;
                    
                    // Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                    touchOrigin.x = -1;
                    
                    // Check if the difference along the x axis is greater than the difference along the y axis.
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                        // If x is greater than zero, set horizontal to 1, otherwise set it to -1
                        horizontal = x > 0 ? 1 : -1;
                    else
                        // If y is greater than zero, set horizontal to 1, otherwise set it to -1
                        vertical = y > 0 ? 1 : -1;
                }
            }

			#endif // End of mobile platform dependendent compilation section started above with #elif
		
		movement = false;

		if(!canMove)
		{
			myRigidbody.velocity = Vector2.zero;
			return;
		}
 
		if(!attack)
		{
			/*if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
			{
				// transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal")
				// * moveSpeed * Time.deltaTime, 0f, 0f));
				myRigidbody.velocity = new Vector2
					(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myRigidbody.velocity.y);
				movement = true;
				lastMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
			}

			if(Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
			{
				// transform.Translate (new Vector3(0f, Input.GetAxisRaw("Vertical")
				// * moveSpeed * Time.deltaTime, 0f));
				myRigidbody.velocity = new Vector2 
					(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
				movement = true;
				lastMovement = new Vector2(0f, Input.GetAxisRaw("Vertical"));
			}

			if(Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
			{
				myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
			}
			
			if(Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
			}*/

			moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

			if(moveInput != Vector2.zero)
			{
				myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
				movement = true;
				lastMovement = moveInput;

//				if(knockBackCount <= 0)
//				{
//					myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
//					movement = true;
//					lastMovement = moveInput;
//				}
//				else
//				{
//					if(knockFromRight)
//						myRigidbody.velocity = new Vector2(-knockBack, knockBack);
//					if(!knockFromRight)
//						myRigidbody.velocity = new Vector2(knockBack, knockBack);
//					knockBackCount -= Time.deltaTime;
//				}
			}
			
			else 
			{
				myRigidbody.velocity = Vector2.zero;
			}

			// Attacking
			if(Input.GetKeyDown(KeyCode.Space)) 
			{
				attackTimeCounter = attackTime;
				attack = true;
 				//myRigidbody.AddForce(moveInput * moveSpeed); (if you want to move while attacking)
				myRigidbody.velocity = Vector2.zero;
				animator.SetBool("attack", true);

				sfxMan.playerAttack.Play();
			}

			/*if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
			{
				currentMoveSpeed = moveSpeed * diagonalMoveModifier;
			}
			else
			{
				currentMoveSpeed = moveSpeed;
			}*/
		}

		if (attackTimeCounter > 0)
		{
			attackTimeCounter -= Time.deltaTime;
		}

		if (attackTimeCounter <= 0)
		{
			attack = false;
			animator.SetBool("attack", false);
		}
	
		animator.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
		animator.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
		animator.SetBool("movement", movement);
		animator.SetFloat("lastX", lastMovement.x);
		animator.SetFloat("lastY", lastMovement.y);

		if(!flash)
		{
			// Camera Flash
			if(Input.GetKeyDown(KeyCode.Q)) 
			{
				flashTimeCounter = flashTime;
				flash = true;
 				//myRigidbody.AddForce(moveInput * moveSpeed); (if you want to move while attacking)
				myRigidbody.velocity = Vector2.zero;
				animator.SetBool("flash", true);

				sfxMan.playerAttack.Play();
			}

			/*if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
			{
				currentMoveSpeed = moveSpeed * diagonalMoveModifier;
			}
			else
			{
				currentMoveSpeed = moveSpeed;
			}*/
		}

		if (flashTimeCounter > 0)
		{
			flashTimeCounter -= Time.deltaTime;
		}

		if (flashTimeCounter <= 0)
		{
			flash = false;
			animator.SetBool("flash", false);
		}
	}
}
 