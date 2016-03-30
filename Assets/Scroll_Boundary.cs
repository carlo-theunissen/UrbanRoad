using UnityEngine;
using System.Collections;

public class Scroll_Boundary : MonoBehaviour {

	// Use this for initialization
		
	void Update () {
			if (transform.position.x > 1000) {
				transform.position = new Vector2 (999, 0);
			}
		}
	
	}
