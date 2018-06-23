using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;

	private bool flashActive;
	public float flashLength;
	private float flashCounter;

	private SpriteRenderer enemySprite;

	public string enemyQuestName;
	private QuestManager theQM;

	private SFXManager sfxMan;

	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;

		theQM = FindObjectOfType<QuestManager> ();
		sfxMan = FindObjectOfType<SFXManager>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (currentHealth <= 0)
		{
			sfxMan.enemyDead.Play();
			theQM.enemyKilled = enemyQuestName;
			Destroy(gameObject);
		}

//		if (flashCounter > flashLength * .66f)
//			{
//				enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);							
//			}
//
//			else if (flashCounter > flashLength * .33f)
//			{
//				enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
//			}
//
//			else if (flashCounter > 0f)
//			{
//				enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
//			}
//						
//			else 
//			{
//				enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
//				flashActive = false;
//			}
//				flashCounter -= Time.deltaTime;
	}

	public void attackEnemy(int damageGiven)
	{
		currentHealth -= damageGiven;

//		flashActive = true;
//		flashCounter = flashLength;
		
		sfxMan.enemyHurt.Play();
	}

	public void setMaxHealth()
	{
		currentHealth = maxHealth;
	}
}
