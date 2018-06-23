using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	
	public GameObject dBox;
	public Text dText;

	public bool dialogueActive;

	public string[] dialogueLines;
	public int currentLine;

	private playerController thePlayer;
	private EnemyController theEnemy;
	private AI theAI;

	
	// Use this for initialization
	void Start () 
	{
		thePlayer = FindObjectOfType<playerController>();
		theEnemy = FindObjectOfType<EnemyController>();
		theAI = FindObjectOfType<AI> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(dialogueActive && Input.GetKeyUp(KeyCode.E))
		{
			//dBox.SetActive(false);
			//dialogueActive = false;

			currentLine++;
		}

		if(currentLine >= dialogueLines.Length)
		{
			dBox.SetActive(false);
			dialogueActive = false;

			currentLine = 0;
			thePlayer.canMove = true;
			theEnemy.canMove = true;
			theAI.canMove = true;
		}

		dText.text = dialogueLines[currentLine];
		
	}

	public void showBox(string dialogue) 
	{

		dialogueActive = true;	
		dBox.SetActive(true);
		dText.text = dialogue;
	}

	public void showDialogue() 
	{
		dialogueActive = true;	
		dBox.SetActive(true);
		thePlayer.canMove = false;
		theEnemy.canMove = false;
		theAI.canMove = false;
	}

	
}
