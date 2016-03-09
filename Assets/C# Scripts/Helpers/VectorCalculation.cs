using System;
using UnityEngine;

public class VectorCalculation
{
	public static Vector2 rotateVector(Vector2 old, float deg){
		deg = 360 - deg;
		float rad = deg * Mathf.Deg2Rad,
		cs = Mathf.Cos (rad),
		sn = Mathf.Sin (rad),
		x = old.x * cs - old.y * sn,
		y = old.x * sn + old.y * cs;
		return new Vector2 (Mathf.Round(x), Mathf.Round(y));
	}
	public static Vector2 revertToOrigin(int x, int y, Level level){
		return new Vector2 (x, (level.getHeight () - y - 1));
	}
	public static  Vector2 revertToOrigin(Vector2 old, Level level){
		return revertToOrigin ((int) old.x, (int) old.y, level);
	}
}


