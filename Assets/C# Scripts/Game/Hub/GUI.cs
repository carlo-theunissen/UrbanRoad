using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Game
{
	public class GUI : MonoBehaviour
	{
	    
		//public static int boxRightWidth;
		public Mouse mouse;
        public GameObject canvas;
        public GameObject button;
		private Dictionary<int,List<Block>> blocks;
		private List<GameObject> buttons;
		public GameObject reloadBut;
		public float getMenuWidth(){
			RectTransform trans = canvas.transform as RectTransform;
			return trans.rect.width;
		}
		private float getReloadButHeight(){
			RectTransform trans = reloadBut.transform as RectTransform;
			return trans.rect.height;
		}
		void Start(){
	
			getBlocks();
			buttons = new List<GameObject> ();
			int index = 0;
			foreach(KeyValuePair<int,List<Block>> blockList in blocks){
				makeBlock (blockList.Value.ToArray (), index++);

			}


	    }
        
		private void makeBlock(Block[] blocks, int index){
            GameObject workingButton = UnityEngine.Object.Instantiate(button);
			workingButton.transform.position = getPosition (index);
            RectTransform t = workingButton.transform as RectTransform;
            t.sizeDelta = getDimensions();
            
            workingButton.transform.SetParent(canvas.transform, false);

			Button buttonController =  (Button) workingButton.AddComponent<Button> ();

			buttonController.blocks = blocks;
			buttonController.dimensions = t.sizeDelta;
			buttonController.prefab = button;

			buttons.Add (workingButton);
        }

		private Vector3 getPosition(int index){
			Vector2 dimensions = getDimensions ();
			if (index > 4) {
				return new Vector2 (dimensions.x + 20, ((dimensions.y + 7) * -(index - 5) - getReloadButHeight() * 1.5f));
			}
			return new Vector2 (10, (-(dimensions.y + 7)* index - getReloadButHeight() * 1.5f ));
		}


        private Vector2 getDimensions(){
            RectTransform t = canvas.transform as RectTransform;
			return new Vector2(t.sizeDelta.x / 2.5f, (float) ((t.rect.height * t.localScale).y / 6));
        }
	    private void getBlocks()
	    {
			if (blocks == null) {

				blocks = new Dictionary<int,List<Block>> ();
				Level level = GameMode.getCurrentLevel ();
				foreach (Block block in  level.getBlocks()) {
					if (!blocks.ContainsKey (block.getId ())) {
						blocks.Add (block.getId (), new List<Block> ());
					}
					blocks [block.getId ()].Add (block);

				}
			}

	    }
			
	}

}