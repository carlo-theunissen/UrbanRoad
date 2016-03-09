using UnityEngine;
using System.Collections;

public class Level {
	private int levelId;
	private int blockWidth;
	private int blockHeight;
	private Block[] blocks;
	private Vector2 startPos;
	private Vector2 endPos;
	private Block[,] grid = null;

	public Level setWidth(int width){
		blockWidth = width;
		return this;
	}

	public Level setHeight(int height){
		blockHeight = height;
		return this;
	}

	public Level setBlocks(Block[] blocks){
		this.blocks = blocks;
		return this;
	}

	public Level setStart(Vector2 start){
		startPos = start;
		return this;
	}

	public Level setEnd(Vector2 end){
		endPos = end;
		return this;
	}
	public Level(int levelId){
		this.levelId = levelId;
	}
	public int getId(){
		return levelId;
	}
	public void clear(){
		grid = new Block[getWidth(),getHeight()];
	}

	public Block[] getBlocks(){
		return blocks;
	}

	public Vector2 getStart(){
		return startPos;
	}
		
	public Vector2 getFinish(){
		return endPos;
	}

	public void saveToDevice(){
	}

	public bool isValidPath(){
		return true;
	}
		
	public Block getPos(int x, int y){
		if (grid == null) {
			clear ();
		}
		return grid [x, y];
	}
	public Block getPos(Vector2 pos){
		if (grid == null) {
			clear ();
		}
		return getPos ((int) pos.x, (int) pos.y);
	}

	public void removeBlock(Block block){
		for (int x = 0; x < grid.GetLength(0); x++) {
			for (int y = 0; y < grid.GetLength(1); y++) {
				if (grid [x, y] != null && grid [x, y].Equals( block )) {
					grid [x, y] = null;
				}
			}
		}

		block
			.clearPos ()
			.setRotation (0);
	}

	private void printGrid(){
		for (int y = 0; y < blockHeight; y++) {
			string row = "";
			for(int x = 0; x < blockWidth; x++){
				row += getPos (x, y) == null ? "0" : "*";
			}
			Debug.Log (row);
		}
	}

	public bool setBlock(int x, int y, Block block, float deg=0){
		if (grid == null) {
			clear ();
		}

		removeBlock (block);

		Vector2 start = new Vector2 (x, y);
		Vector2 least = new Vector2();
		bool calculated = false;
		foreach(Vector2 col in block.getCollision()){
			Vector2 temp = VectorCalculation.rotateVector (col, deg);
			if (!calculated) {
				least = temp;
				calculated = true;
			}
			if (temp.x < least.x) {
				least.x = temp.x;
			}
			if (temp.y < least.y) {
				least.y = temp.y;
			}
		}
		least.x = Mathf.Abs (least.x);
		least.y = Mathf.Abs (least.y);

		foreach(Vector2 col in block.getCollision()){
			Vector2 temp = start +  VectorCalculation.rotateVector(col, deg) + least;
			temp = VectorCalculation.revertToOrigin (temp, this);
		
			grid [(int)temp.x, (int)temp.y] = block;
		}


		saveToDevice ();
		printGrid (); 
		return true;
	}


	public bool canSet(int x, int y, Block block, float deg=0){
		Vector2 start = new Vector2 (x, y);
		if(x >= getWidth() || y >= getHeight() || getPos(start) != null) { 
			return false; 
		}

		foreach(Vector2 col in block.getCollision()){
			Vector2 temp = VectorCalculation.revertToOrigin( start +  VectorCalculation.rotateVector(col, deg) , this);
			if(temp.x >= getWidth() || temp.y >= getHeight() || getPos(temp) != null) {
				return false; 
			}
		}

		return true;
	}

	public RoadPiece[] getRoad(){
		return new RoadPiece[2];
	}

	public int getHeight(){
		return blockHeight;
	}
	public int getWidth(){
		return blockWidth;
	}

}
