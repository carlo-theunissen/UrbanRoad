using UnityEngine;
using System.Collections;
namespace Game
{
	public class Mouse : MonoBehaviour {

		private Block piece;
	    private bool following = false;
		public GUI gui;
		public BlockPlacer placer;
		private int deg = 0;
		private Vector2? lastGridPos;
		private Vector2? lastRawPos;
		private float menuWide;

		void Start(){
			menuWide = gui.getMenuWidth ();
		}

		public void setDeg(int deg){
			this.deg = deg;
			lastRawPos = null;
		}
		public int getDeg(){
			return deg;
		}
		public void setPiece(Block block){
			piece = block;
		}

	    public void setFollowing(bool following)
	    {
	        this.following = following;
	    }

		public bool getFollowing(){
			return following;
		}

		public Vector2? getMousePos(){
			if (Input.touchCount > 0) {
				return Input.GetTouch (0).position;
			}

			if (Input.GetMouseButton (0)) {
				return Input.mousePosition;
			}
			return null;
		}
			

		// Update is called once per frame
		void Update () {
			if (!following) {
				return;
			}

			Vector2? mousePos = getMousePos ();
			if (lastRawPos == mousePos) {
				return;
			}
			lastRawPos = mousePos;
			if (mousePos != null && piece != null) {
				lastGridPos = gridPos ((Vector2) mousePos);
				placer.hover(piece, (Vector2) lastGridPos, deg);

			} else {
				placeObject ();
				following = false;
			}
		}

		private void placeObject(){
			if (lastGridPos != null && piece != null) {
				placer.placeObject (piece, (Vector2) lastGridPos, deg);
			}
		}
			
			

		private Vector2 gridPos(Vector2 pos){
			int width = GameMode.getCurrentLevel ().getWidth ();
			int height = GameMode.getCurrentLevel ().getHeight ();

			float x = Mathf.Round( pos.x / ((Screen.width - menuWide) / width)) -1;
			float y = Mathf.Round( pos.y / (Screen.height / height) ) -1;

			x = Mathf.Min (x, width-1);
			x = Mathf.Max (0, x);

			y = Mathf.Min (y, height -1);
			y = Mathf.Max (0, y);
			return new Vector2 (x, y);
		}
	}
}