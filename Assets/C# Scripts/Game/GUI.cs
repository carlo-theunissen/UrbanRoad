using UnityEngine;
using System.Collections;
namespace Game
{
	public class GUI : MonoBehaviour
	{
	    private Block[] blocks = null;
		public static int boxRightWidth;
		public Mouse mouse;
		public BlockPlacer placer;
		private bool rotated;
		private int deg = 0;
		static GUI()
		{         
			boxRightWidth = 150;     
		}   

	    void OnGUI()
	    {

			GUIcalculation.setGuiPos (); //calculates the positions of the fingers
			Rect pos;

	        if(blocks == null)
	        {
	            blocks = getBlocks();
	        }
	        // Make a background box
	        UnityEngine.GUI.Box(new Rect(Screen.width - 150, 0, 150, Screen.height), " " );

			pos = new Rect (Screen.width - 130, 10, 135, 30);
			UnityEngine.GUI.Box (pos, "Reset");
			if (GUIcalculation.collisionWithTouch(pos))
	        {
				placer.clearBlocks ();
				mouse.setFollowing (false);
	        }

	       

			int i = 0;
			foreach (Block block in blocks) {
				pos = new Rect (Screen.width - 130, 45 + i * 150, 135, 150);
				UnityEngine.GUI.Box (new Rect (Screen.width - 130, 45 + i * 150, 135, 150), block.getPlaceholder());
				if ( !mouse.getFollowing () && GUIcalculation.collisionWithTouch (pos)) {

					deg = 0;
					mouse.setDeg (0); 
					mouse.setPiece (block);
					mouse.setFollowing (true);
				}
				i++;
			}


			pos = new Rect (30, Screen.height - 60, 40, 40);
			UnityEngine.GUI.Box (pos, "R");
			if (Input.GetKeyUp ("r") || GUIcalculation.collisionWithTouch(pos)) {
				if (!rotated) {
					AudioProvider.getInstance().playAudio("error");
					rotated = true;
					deg += 90;
					deg %= 360;
					mouse.setDeg (deg);

				}
				
			} else {
				rotated = false;
			}



	    }
	    private Block[] getBlocks()
	    {
			Level level = GameMode.getCurrentLevel();
	        return level.getBlocks();
	    }
	}

}