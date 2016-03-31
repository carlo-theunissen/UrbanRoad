using UnityEngine;
using System.Collections;

public class Scroll_Boundary : MonoBehaviour {

	// Use this for initialization
		
	void Update () {
		if (transform.position.x > 1000) {
			
		}
		RectTransform rect = transform as RectTransform;
//		Debug.Log (rect.localPosition);
	}
	
}
