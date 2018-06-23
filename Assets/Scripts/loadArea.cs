using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadArea : MonoBehaviour {
	
	public string levelToLoad;	
	public string exitPoint;
	private playerController thePlayer;
	public Image imageUI;

	// Use this for initialization
	void Start () 
	{
		thePlayer = FindObjectOfType<playerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "player_1")
		{
			Application.LoadLevel(levelToLoad);
			thePlayer.startPoint = exitPoint;
		}
	}
}
