using UnityEngine;
using System.Collections;

public class Level {
	public void clear(){
	}

	public Block[] getBlocks(){
		return new Block[2];
	}

	public Vector2 getStart(){
		return new Vector2 ();
	}


	public Vector2 getFinish(){
		return new Vector2 ();
	}

	public void saveToDevice(){
	}

	public bool isValidPath(){
		return true;
	}
		
	public Block getPos(int x, int y){
		return null;
	}

	public bool setBlock(int x, int y, Block block){
		return true;
	}

	public bool canSet(int x, int y, Block block){
		return true;
	}

	public RoadPiece[] getRoad(){
		return new RoadPiece[2];
	}

}
