using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Level level = GameMode.getLevel (1);
		Debug.Log (level.getBlocks ().Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
