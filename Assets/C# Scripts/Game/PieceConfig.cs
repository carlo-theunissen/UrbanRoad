using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
	public class PieceConfig
	{
		public int id = 1;
		public int blockCount = 1;
		public List<Vector2> collision = new List<Vector2> ();
		public GameObject prefab = null;
		public Sprite placeholder;
	}


}