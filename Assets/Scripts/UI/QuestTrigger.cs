using UnityEngine;
using System.Collections;

public class QuestTrigger : MonoBehaviour {
	
	private QuestManager theQM;

	public int questNumber;

	public bool startQuest;
	public bool endQuest;
	
	// Use this for initialization
	void Start () {

		theQM = FindObjectOfType<QuestManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.gameObject.name == "player_1")
		{
			if(!theQM.questCompleted[questNumber])
			{
				if(startQuest && !theQM.quests[questNumber].gameObject.activeSelf)
				{
					theQM.quests[questNumber].gameObject.SetActive(true);
					theQM.quests[questNumber].StartQuest();
				}

				if(endQuest && theQM.quests[questNumber].gameObject.activeSelf)
				{
					theQM.quests[questNumber].EndQuest();
				}
			}
		}	
		
	}
}
