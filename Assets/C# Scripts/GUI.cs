using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour
{
    private Block[] blocks = null;
	public static int boxRightWidth;
	public Mouse blockplacer;

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
            //wat doet deze button?
        }
        int i = 0;
        foreach (Block block in blocks)
        {
			if (UnityEngine.GUI.RepeatButton(new Rect(Screen.width - 130, 45 + i * 150, 135, 150), block.getPlaceholder()))
            {

				blockplacer.setDeg (180); 
				blockplacer.setPiece (block);
				blockplacer.setFollowing (true);
            }
            i++;
            
        }

    }
    private Block[] getBlocks()
    {
        Level level = GameMode.getLevel(1);
        return level.getBlocks();
    }
}

