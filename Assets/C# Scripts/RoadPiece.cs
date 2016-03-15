using UnityEngine;
using System.Collections;
public class RoadPiece
{
	public int type = 1;
	public Vector2 Position;
	public GameObject getPrefab(){
		return RoadPieceHelper.getRoadPrefab (type);
	}
}


