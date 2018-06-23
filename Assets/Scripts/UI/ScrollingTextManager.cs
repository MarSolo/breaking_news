using UnityEngine;
using System.Collections;
using TMPro;

public class ScrollingTextManager : MonoBehaviour {

	public TextMeshProUGUI TextMeshProComponent;
	public float scrollSpeed = 10;

	private TextMeshProUGUI m_cloneTextObject;

	private RectTransform m_textRectTransform;
	private string sourceText;
	private string tempText;
	private bool hasTextChanged;

	void Awake(){
		m_textRectTransform = TextMeshProComponent.GetComponent<RectTransform>();

		m_cloneTextObject = Instantiate(TextMeshProComponent) as TextMeshProUGUI;
		RectTransform cloneRectTransform = m_cloneTextObject.GetComponent<RectTransform>();
		cloneRectTransform.SetParent(m_textRectTransform);
		cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
		cloneRectTransform.localScale = new Vector3(1,1,1);
	}

	void OnEnable() { 
	// Subscribe to event fired when text object has been regenerated. 
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED); 
	} 

	void OnDisable() { 
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED); 
	} 

	//checks to see if it the text has changed 
	void ON_TEXT_CHANGED(Object obj) { 
		if (obj == TextMeshProComponent) 
		hasTextChanged = true; 
	} 

	// Use this for initialization
	IEnumerator Start () {
		float width = TextMeshProComponent.preferredWidth * m_textRectTransform.lossyScale.x;
		Vector3 startPosition = m_textRectTransform.position;

		float scrollPosition = 0;

		while (true){
			// Re-Compute the width of the RectTransform if the text object has changed
			if (hasTextChanged) { 
				width = TextMeshProComponent.preferredWidth;
				m_cloneTextObject.text = TextMeshProComponent.text;
			}

			// Scroll the text across the screen by moving the RectTransform
			m_textRectTransform.position = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);

			scrollPosition += scrollSpeed * 20 * Time.deltaTime;

			yield return null;
		}
	}
}
