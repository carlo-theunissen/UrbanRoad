using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	// Use this for initialization
	private SplineController controller;
	void Start () {
		controller = GetComponent<SplineController> ();
		controller.AutoStart = true;
		controller.Animate ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
