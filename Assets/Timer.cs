using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float sec = 15;
	// Use this for initialization
	private SplineController controller;
	void Start () {
		
		controller = GetComponent<SplineController> ();
		StartCoroutine (Starter ());
	}
	IEnumerator Starter(){
		while (true) {
			Debug.Log ("test!");
			yield return new WaitForSeconds (sec);
				GetComponent<SplineInterpolator> ().Restart ();


			//break;
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
