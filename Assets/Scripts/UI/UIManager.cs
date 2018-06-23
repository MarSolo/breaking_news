using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Slider healthBar;
	public Text healthText;
	public playerHealthManager playerHealthManager;

	private static bool UIExists;

	// Use this for initialization
	void Start () 
	{
		if(!UIExists)
		{
			UIExists = true;
			DontDestroyOnLoad(transform.gameObject);
		} 
		else 
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthBar.maxValue = playerHealthManager.playerMaxHealth;
		healthBar.value = playerHealthManager.playerCurrentHealth;
		healthText.text = "CREDIBILITY " /* +  playerHealthManager.playerCurrentHealth 
						+ "/" + playerHealthManager.playerMaxHealth*/ ; 
	}
}
