using UnityEngine;
using System.Collections;
public class RoadPiece
{
	public int type = 1;
	public Vector2 Position;
	private GameObject displayedObject;
	public GameObject getPrefab(){
		if (displayedObject == null) {
			displayedObject = Object.Instantiate (RoadPieceHelper.getRoadPrefab (type));
		}
		return displayedObject;
	}
}


