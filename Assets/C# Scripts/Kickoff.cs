using UnityEngine;
using System.Collections;

public class Kickoff : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Level level = GameMode.getLevel(1);
        Block[] blocks = level.getBlocks();
        foreach (Block block in blocks)
        {
            Instantiate(block.getBlueprintPrefab(), new Vector3(0.0f, 0.25f, 0.0f), Quaternion.identity);
        }

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
