using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Level level = GameMode.getLevel (1);
		Block[] blocks = level.getBlocks ();
		Debug.Log (level.canSet (0, 0, blocks [0]));
		level.setBlock (0, 0, blocks [0]);
		Debug.Log (level.canSet (1, 1, blocks [0]));
		level.removeBlock (blocks [0]);
		Debug.Log (level.canSet (1, 1, blocks [0]));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
