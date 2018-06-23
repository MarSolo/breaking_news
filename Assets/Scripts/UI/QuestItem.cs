using UnityEngine;
using System.Collections;

public class QuestItem : MonoBehaviour {
	
	private QuestManager theQM;	
	
	public int questNumber;

	public string itemName;
	

	// Use this for initialization
	void Start () 
	{
		theQM = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.gameObject.name == "player_1")
		{
			if(!theQM.questCompleted[questNumber] && theQM.quests[questNumber].gameObject.activeSelf)
			{
				theQM.itemCollected = itemName;
				gameObject.SetActive(false);
			}
		}	
		
	}
}
