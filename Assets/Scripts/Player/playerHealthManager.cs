using UnityEngine;
using System.Collections;

public class playerHealthManager : MonoBehaviour {

	public int playerMaxHealth;
	public int playerCurrentHealth;

	private bool flashActive;
	public float flashLength;
	private float flashCounter;

	private SpriteRenderer playerSprite;

	private SFXManager sfxMan;

	// private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () 
	{
		playerCurrentHealth = playerMaxHealth;
		playerSprite = GetComponent<SpriteRenderer>();
		// myRigidbody = GetComponent<Rigidbody2D>();
		sfxMan = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerCurrentHealth < 0)
		{
			sfxMan.playerDead.Play();
			gameObject.SetActive(false);
		}

			if (flashCounter > flashLength * .66f)
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);							
			}

			else if (flashCounter > flashLength * .33f)
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
			}

			else if (flashCounter > 0f)
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
			}
						
			else 
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				flashActive = false;
			}
				flashCounter -= Time.deltaTime;
	}

	public void damagePlayer(int damageGiven)
	{
		playerCurrentHealth -= damageGiven;

		flashActive = true;
		flashCounter = flashLength;

		sfxMan.playerHurt.Play();
	}

	public void setMaxHealth()
	{
		playerCurrentHealth = playerMaxHealth;
	}
}
