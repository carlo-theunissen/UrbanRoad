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


	public void placeRoad(float x, float z, GameObject road){
		road.transform.position = new Vector3 (x, 0.1f, z);
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
		RoadPiece pos = getRoadPiece (level.getFinish ());
		placeRoad (pos.Position.x, pos.Position.y, pos.getPrefab ());

		pos = getRoadPiece (level.getStart ());
		placeRoad (pos.Position.x, pos.Position.y, pos.getPrefab ());
	}

	private RoadPiece getRoadPiece(Vector2 calc){
		RoadPiece road = new RoadPiece();
		if (calc.x == 0) {
			calc.x--;
			road.Position = calc;
			road.type = 2;
			return road;
		}
		if (calc.y == 0) {
			calc.y--;
			road.Position = calc;
			road.type = 2;
			return road;
		}
		if (calc.x + 1 == level.getWidth ()) {
			calc.x++;
			road.Position = calc;
			road.type = 1;
			return road;
		}
		if (calc.y + 1 == level.getHeight ()) {
			calc.y++;
			road.Position = calc;
			road.type = 1;
			return road;
		}
		road.Position = calc;
		return road;
	}
}
