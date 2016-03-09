using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	private Block piece;
    private bool active = false;
	public Grid grid;
	private int deg = 0;


	public void setPiece(Block block){
		piece = block;
	}
    public void setActive(bool active)
    {
		Debug.Log (active);
        this.active = active;
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
	// Update is called once per frame
	void Update () {
		if (!active) {
			return;
		}

		Vector2? pos = getMousePos ();

		if (pos != null && piece != null) {
			Vector2 location = gridPos ((Vector2)pos);
			Debug.Log (location);
			Vector2 temp = transformToGrid (location);
			Debug.Log (temp);
			grid.placeDummy (temp.x, temp.y, piece.getBlueprintPrefab(), deg);
		}
	}

	/**
	 * Verkrijgt de linker boven hoek in pos en vertaald dat naar een vector zodat het object goed staat
	 */ 
	private Vector2 transformToGrid(Vector2 pos){ 
		float width = 0;
		float height = 0;

		foreach(Vector2 collision in piece.getCollision ()){
			Vector2 temp = VectorCalculation.rotateVector (collision, deg);
			width = Mathf.Max (width, Mathf.Abs(temp.x));
			height = Mathf.Max (height, Mathf.Abs(temp.y));
		}

		return pos - new Vector2 ((width+1)/2, (height+1)/2);

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
