using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	public AudioSource playerHurt;
	public AudioSource playerDead;
	public AudioSource playerAttack;
	//public AudioSource flashAttack;
	public AudioSource enemyDead;
	public AudioSource enemyHurt;
	public AudioSource textMessage;

	private static bool sfxManExists;

	// Use this for initialization
	void Start () {

		if(!sfxManExists)
		{
			sfxManExists = true;
			DontDestroyOnLoad(transform.gameObject);
		} 
		else 
		{
			Destroy(gameObject);
		}

		
	}
}
