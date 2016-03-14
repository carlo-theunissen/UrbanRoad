﻿using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour
{
    private Block[] blocks = null;
	public static int boxRightWidth;
	public Mouse blockplacer;
	private bool rotated;
	private int deg = 0;
	static GUI()
	{         
		boxRightWidth = 150;     
	}   

    void OnGUI()
    {
        if(blocks == null)
        {
            blocks = getBlocks();
        }
        // Make a background box
        UnityEngine.GUI.Box(new Rect(Screen.width - 150, 0, 150, Screen.height), " " );
        if (UnityEngine.GUI.Button(new Rect(Screen.width - 130, 10, 135, 30), "Reset"))
        {
			foreach (Block block in blocks) {
				block.removeBlueprintPrefab ();
			}
			GameMode.getCurrentLevel ().clear ();
			blockplacer.setFollowing (false);
        }

        int i = 0;
        foreach (Block block in blocks)
        {
			if (UnityEngine.GUI.RepeatButton(new Rect(Screen.width - 130, 45 + i * 150, 135, 150), block.getPlaceholder()))
            {
				deg = 0;
				blockplacer.setDeg (0); 
				blockplacer.setPiece (block);
				blockplacer.setFollowing (true);
            }
            i++;
        }

		if (Input.GetKeyUp ("r") || UnityEngine.GUI.Button (new Rect (30, Screen.height - 60, 40, 40), "R")) {
			if (!rotated) {
				rotated = true;
				deg += 90;
				deg %= 360;
				blockplacer.setDeg (deg);

			}
			
		} else {
			rotated = false;
		}

		if (Input.GetKeyUp ("p")) {
			Debug.Log ("DDSFDFSFSDF");
			GameMode.getCurrentLevel ().printGrid ();
			Debug.Log ("DDSFDFSFSDF");
		}



    }
    private Block[] getBlocks()
    {
		Level level = GameMode.getCurrentLevel();
        return level.getBlocks();
    }
}
