using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    private GameObject follow;
    private bool active = false;
	public Grid grid;

    public void setFollow(GameObject follow)
    {
        this.follow = follow;
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

		if (pos != null && follow != null) {
			Vector2 temp = transformToGrid ((Vector2)pos);
			grid.place ((int)temp.x, (int)temp.y, follow);
		}
	
	}
	private Vector2 transformToGrid(Vector2 pos){
		int width = GameMode.getCurrentLevel ().getWidth ();
		int height = GameMode.getCurrentLevel ().getHeight ();

		float x = Mathf.Round( pos.x / ((Screen.width - GUI.boxRightWidth) / width));
		float y = Mathf.Round( pos.y / (Screen.height / height) );

		x = Mathf.Min (x, width-1);
		x = Mathf.Max (0, x);

		y = Mathf.Min (y, height -1);
		y = Mathf.Max (0, y);
		return new Vector2 (x, y);
	}
}
