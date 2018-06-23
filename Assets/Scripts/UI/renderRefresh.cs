using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderRefresh : MonoBehaviour {

private SpriteRenderer tempRend;
 
    void Start () 
	{
        tempRend = GetComponent<SpriteRenderer>();
    }
 
 
    void LateUpdate () 
	{ 
        tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint (tempRend.bounds.min).y * -1;
    }
}
