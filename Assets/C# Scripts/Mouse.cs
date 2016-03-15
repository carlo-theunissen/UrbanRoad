using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	private Block piece;
    private bool following = false;
	private Vector2 dimensions;
	public Grid grid;
	private int deg = 0;
	private Vector2? lastGridPos;

	public void setDeg(int deg){
		this.deg = deg;
		setWidthHeight ();
	}
	public void setPiece(Block block){
		piece = block;
		setWidthHeight ();
	}
    public void setFollowing(bool following)
    {
        this.following = following;
    }
	private Vector2? getMousePos(){
		if (Input.touchCount > 0) {
			return Input.GetTouch (0).position;
		}

		if (Input.GetMouseButton (0)) {
			return Input.mousePosition;
		}
		return null;
	}

	private bool canPlacePiece(Vector2 pos){
		
		Level level = GameMode.getCurrentLevel ();
		if (pos.x < 0 || pos.y < 0 || pos.x + dimensions.x  > level.getWidth () || pos.y + dimensions.y > level.getHeight() ) {
			return false;
		}
		level.removeBlock (piece);
		return level.canSet((int) pos.x,(int) pos.y,piece,deg);
	}

	// Update is called once per frame
	void Update () {
		if (!following) {
			return;
		}

		Vector2? mousePos = getMousePos ();

		if (mousePos != null && piece != null) {
			
			lastGridPos = gridPos ((Vector2)mousePos);
			Vector2 temp = transformToGrid ((Vector2)lastGridPos);
			GameObject prefab = piece.getBlueprintPrefab ();

			Renderer rend = prefab.GetComponent<Renderer> ();
			rend.material.shader = Shader.Find ("Specular");
			rend.material.SetColor ("_SpecColor", canPlacePiece ((Vector2)lastGridPos) ? Color.white : Color.red);
			grid.placeDummy (temp.x, temp.y, prefab, deg);
		} else {
			placeObject ();
			following = false;
		}
	}

	private void placeObject(){
		if (lastGridPos != null && piece != null) {
			Vector2 temp = (Vector2)lastGridPos;
			if (canPlacePiece (temp)) {
				GameMode.getCurrentLevel ().setBlock ((int) temp.x, (int) temp.y, piece, deg);
				drawRoad ();
			} else {
				GameMode.getCurrentLevel ().removeBlock (piece);
				piece.removeBlueprintPrefab ();
			}
		}
	}

	private void drawRoad(){
		Level level = GameMode.getCurrentLevel ();
		if (level.containsAllBlocks ()) {
			RoadPiece[] pieces = level.getRoad ();
			if (pieces != null) {
				foreach (RoadPiece road in pieces) {
					grid.placeRoad (road.Position.x, road.Position.y, road.getPrefab ());
				}
			}
		}
	}

	private void setWidthHeight(){
		if (piece == null) {
			return;
		}
		dimensions = piece.getWidthHeight (deg);
	}

	/**
	 * Verkrijgt de linker boven hoek in pos en vertaald dat naar een vector zodat het object goed staat
	 */ 
	private Vector2 transformToGrid(Vector2 pos){ 
		return pos + new Vector2 (dimensions.x  / 2 - 0.5f, dimensions.y / 2 - 0.5f);

	}
	private Vector2 gridPos(Vector2 pos){
		int width = GameMode.getCurrentLevel ().getWidth ();
		int height = GameMode.getCurrentLevel ().getHeight ();

		float x = Mathf.Round( pos.x / ((Screen.width - GUI.boxRightWidth) / width)) -1;
		float y = Mathf.Round( pos.y / (Screen.height / height) ) -1;

		x = Mathf.Min (x, width-1);
		x = Mathf.Max (0, x);

		y = Mathf.Min (y, height -1);
		y = Mathf.Max (0, y);
		return new Vector2 (x, y);
	}
}
