using UnityEngine;
using System.Collections;

namespace Game
{
	public class RoadPlacer
	{
		private RoadPiece[] data;
		private int current = 0;
		private const int speed = 4;
		public RoadPlacer (RoadPiece[] data)
		{
			this.data = data;
		}
		public GameObject getPrefab(){
			return data [current].getPrefab ();
		}
		public IEnumerator Tick(){
			while (current < data.Length) {
				if (getPrefab ().transform.parent == null) {
					//@dyhart hier begint hij met het plaatsen van een nieuw blok
					setParent ();
				}
				while (needRotation() ) {
					getPrefab ().transform.parent.Rotate (getRotation() * speed);
					yield return null;
				}

				current++;
			}

			//@dyhart hier is het level finished
			
		}
		private bool needRotation(){
			switch (data [current].flipFrom) {
			case Direction.UP:
				return getPrefab ().transform.parent.eulerAngles.x < 180;

				case Direction.LEFT:
				return getPrefab ().transform.parent.eulerAngles.z < 180;

			case Direction.BOTTOM:
				return getPrefab ().transform.parent.eulerAngles.x > 180 || getPrefab ().transform.parent.eulerAngles.x == 0;

			case Direction.RIGHT:
				return getPrefab ().transform.parent.eulerAngles.z > 180 || getPrefab ().transform.parent.eulerAngles.z == 0;
			}
			return false;
		}
		private Vector3 getRotation(){
			switch (data [current].flipFrom) {
			case Direction.UP:
				return new Vector3 (1, 0, 0);

			case Direction.LEFT:
				return new Vector3 (0,0, 1);

			case Direction.BOTTOM:
				return new Vector3 (-1,0, 0);

			case Direction.RIGHT:
				return  new Vector3 (0,0, -1);

			}
			return new Vector3 ();
		}
		private void setParent(){
			GameObject empty = new GameObject ();

			Vector3 dir = new Vector3();
			switch (data [current].flipFrom) {
			case Direction.UP:
				dir = new Vector3 (0, 0, -.5f);
				break;
			case Direction.LEFT:
				dir = new Vector3 (0.5f,0, 0);
				break;
			case Direction.BOTTOM:
				dir = new Vector3 (0,0, 0.5f);
				break;
			case Direction.RIGHT:
				dir = new Vector3 (-0.5f,0, 0);
				break;
			}
			Vector3 lastPos = makeVector3 (VectorCalculation.revertToOrigin (data [current].Position, GameMode.getCurrentLevel ())) + dir;;
			lastPos.y = 0.1f;
			empty.transform.position = lastPos;
			getPrefab ().transform.position = new Vector3 (0, 0, 0);
			getPrefab ().transform.eulerAngles = new Vector3(0, data[current].getRotation(),180);
			getPrefab ().transform.parent = empty.transform;
			getPrefab ().transform.localPosition = dir;



		}
		private Vector3 makeVector3(Vector2 data){
			return new Vector3 (data.x, 0, data.y);
		}
		private Vector3 revertToOrigin(Vector3 loc){
			Vector2 temp = new Vector2 (loc.x, loc.z);
			temp = VectorCalculation.revertToOrigin (temp, GameMode.getCurrentLevel());
			return makeVector3 (temp);
		}
	}
}

