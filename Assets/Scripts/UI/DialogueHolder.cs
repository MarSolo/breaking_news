using UnityEngine;
using System.Collections;

public class DialogueHolder : MonoBehaviour {

	public string dialogue;
	private DialogueManager dMAn;

	public string[] dialogueLines;

	private SFXManager sfxMan;

	// Use this for initialization
	void Start () 
	{
		dMAn = FindObjectOfType<DialogueManager>();
		sfxMan = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "player_1")
		{
			if(Input.GetKeyUp(KeyCode.E))
			{
				//dMAn.showBox(dialogue);
				sfxMan.textMessage.Play();

				if(!dMAn.dialogueActive)
				{
					dMAn.dialogueLines = dialogueLines;
					dMAn.currentLine = 0;
					dMAn.showDialogue();
				}

				if (transform.parent.GetComponent<NPC_movement>() != null)
				{
					transform.parent.GetComponent<NPC_movement>().canMove = false;
				}
			}
		}
	}

}