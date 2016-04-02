using UnityEngine;
using System.Collections;
namespace Game
{
	public class RoadPiece
	{
		public int type = 1;
		public Vector2 Position;
		public Direction flipFrom = Direction.UP;

		private GameObject displayedObject;
		private int? rotation;

		public bool isPrefabActive(){
			return displayedObject != null && displayedObject.activeInHierarchy;
		}

		public GameObject getPrefab(){
			if (!isPrefabActive()) {
				displayedObject = Object.Instantiate (RoadPieceHelper.getRoadPrefab (type));
			}
			return displayedObject;
		}
		public void deletePrefab(){
			if (isPrefabActive()) {
				Object.Destroy (displayedObject);
			}
		}
		public int getRotation(){
			if (rotation == null) {
				rotation = RoadPieceHelper.getRotation (type);
			}
			return (int)rotation;
		}
	}


}