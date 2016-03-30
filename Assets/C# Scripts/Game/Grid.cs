using UnityEngine;
using System.Collections;
namespace Game
{
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
				place.transform.rotation = Quaternion.Euler (place.transform.rotation.x, deg, place.transform.rotation.z);
			
			}

		}

		public void placeObject(float x, float z, GameObject place, float deg = 0 ){
			placeDummy (x, z, place, deg);
		}


		public void placeRoad(float x, float z, GameObject road, float deg,  bool moveY = true){
			road.transform.position = new Vector3 (x, moveY? 0.1f : 0, z);
			road.transform.rotation = Quaternion.Euler (0, deg, 0);
		}

		// Use this for initialization
		void Start ()
	    {
			level = GameMode.getCurrentLevel ();
			makeGrid ();
			displayRoadPoints ();
		}

		private void displayRoadPoints(){
			placeRoad(VectorCalculation.revertToOrigin (level.getFinish (), level));
			placeRoad(VectorCalculation.revertToOrigin (level.getStart (), level));

		}

		private void placeRoad(Vector2 start){
			RoadPiece piece = new RoadPiece();
			piece.type = 1;

			RoadPiece piece2 = new RoadPiece();
			piece.type = 1;


			Vector2 pos = start + getRoadChange (start);
			placeRoad (pos.x, pos.y, piece.getPrefab (),0);

			pos = pos + getRoadChange (start);
			placeRoad (pos.x, pos.y, piece2.getPrefab (),0);
		}

		private Vector2 getRoadChange(Vector2 calc){
			if (calc.y == 0) {
				return new Vector2 (0, -1);
			}
			if (calc.y + 1 == level.getHeight ()) {
				return new Vector2 (0, 1);
			}
			return new Vector2(0,0);
		}
	}
}