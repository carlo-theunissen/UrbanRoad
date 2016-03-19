using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public GameObject plane;
	public GameObject road;


	private Level level;

	private void makeGrid(){

		for (int x = 0; x < level.getWidth (); x++) 
		{
			for (int z = 0; z < level.getHeight (); z++)
			{
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(x,
					0, z);
			}
		}
	}


	public void placeDummy(float x, float z, GameObject place, float deg = 0 ){


		place.transform.position = new Vector3 (x, 0, z);
		if (place.transform.rotation.y != deg) {
			place.transform.rotation = Quaternion.Euler (0, deg, 0);
		
		}

	}


	public void placeRoad(float x, float z, GameObject road, bool moveY = true){
		road.transform.position = new Vector3 (x, moveY? 0.1f : 0, z);
	}

	// Use this for initialization
	void Start ()
    {
		level = GameMode.getCurrentLevel ();
		makeGrid ();
		displayRoadPoints ();
	}

	private void displayRoadPoints(){
		RoadPiece pos = getRoadPiece (VectorCalculation.revertToOrigin(level.getFinish (), level));
		placeRoad (pos.Position.x, pos.Position.y, pos.getPrefab ());

		pos = getRoadPiece (VectorCalculation.revertToOrigin(level.getStart (), level));
		placeRoad (pos.Position.x, pos.Position.y, pos.getPrefab ());
	}

	private RoadPiece getRoadPiece(Vector2 calc){
		RoadPiece road = new RoadPiece();
		if (calc.y == 0) {
			calc.y--;
			road.Position = calc;
			road.type = 2;
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
