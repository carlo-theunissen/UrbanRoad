using UnityEngine;
using System.Collections;

namespace Game
{
	
	public class ObjectSelecter : MonoBehaviour
	{
		public Mouse mouse;
		private Level level;
		void Start(){
			level = GameMode.getCurrentLevel ();
		}
		void Update(){
			Vector2? mousePos = mouse.getMousePos ();
			if (!mouse.getFollowing () && mousePos != null) {
				RaycastHit hit; 
				Ray ray = Camera.main.ScreenPointToRay((Vector2) mousePos); 
				if ( Physics.Raycast (ray,out hit,10f)) {
					GameObject prefab = hit.collider.gameObject;
					foreach (Block block in level.getPlacedBlocks()) {
						if (block.getBlueprintPrefab ().Equals (prefab)) {
							

							mouse.setFollowing (true);
							mouse.setDeg ((int) block.getRotation());
							mouse.setPiece (block);
							break;
						}
					}
				}
			}
		}

	}
}

