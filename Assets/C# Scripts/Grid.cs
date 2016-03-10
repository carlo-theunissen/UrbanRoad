using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public GameObject plane;
	public GameObject road;

    private GameObject[,] grid;
	private Level level;

	private void makeGrid(){

		for (int x = 0; x < level.getWidth (); x++) 
		{
			for (int z = 0; z < level.getHeight (); z++)
			{
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(x,
					0, z);
				grid[x, z] = gridPlane;
			}
		}
	}


	public void placeDummy(float x, float z, GameObject place, float deg = 0 ){


		place.transform.position = new Vector3 (x, 0, z);
		if (place.transform.rotation.y != deg) {
			place.transform.rotation = Quaternion.Euler (0, deg, 0);
		
		}

	}

	// Use this for initialization
	void Start ()
    {
		level = GameMode.getCurrentLevel ();
		grid = new GameObject[level.getWidth (), level.getHeight ()];
		makeGrid ();
		displayRoadPoints ();
	}

	private void displayRoadPoints(){
		Vector2 pos = getOutsidePos (level.getFinish ());
		Instantiate(road, new Vector3(pos.x, 0, pos.y) , Quaternion.identity);
		pos = getOutsidePos (level.getStart ());
		Instantiate(road,  new Vector3(pos.x, 0, pos.y), Quaternion.identity);
	}

	private Vector2 getOutsidePos(Vector2 calc){
		if (calc.x == 0) {
			calc.x--;
			return calc;
		}
		if (calc.y == 0) {
			calc.y--;
			return calc;
		}
		if (calc.x + 1 == level.getWidth ()) {
			calc.x++;
			return calc;
		}
		if (calc.y + 1 == level.getHeight ()) {
			calc.y++;
			return calc;
		}
		return calc;
	}
}
