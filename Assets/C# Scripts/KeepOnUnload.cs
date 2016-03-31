using UnityEngine;
using System.Collections;

public class KeepOnUnload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}

}
