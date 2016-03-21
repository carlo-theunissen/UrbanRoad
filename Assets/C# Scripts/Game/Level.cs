using UnityEngine;
using System.Collections.Generic;
namespace Game
{
	public class Level {
		private int levelId;
		private int blockWidth;
		private int blockHeight;
		private Block[] blocks;
		private Vector2 startPos;
		private Vector2 endPos;
		private Block[,] grid = null;

		private bool hasChanged = false;
		private RoadPiece[] road;
		private List<Block> placedBlocks;

		public Level(int levelId){
			this.levelId = levelId;
			placedBlocks = new List<Block> ();
		}
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
		public int getId(){
			return levelId;
		}
		public void clear(){
			hasChanged = true;
			grid = new Block[getWidth(),getHeight()];
		}

		public Block[] getBlocks(){
			return blocks;
		}
		/**
		 * Returns a Vector2 relative to TOP LEFT
		 */ 
		public Vector2 getStart(){
			return startPos;
		}
			
		/**
		 * Returns a Vector2 relative to TOP LEFT
		 */ 
		public Vector2 getFinish(){
			return endPos;
		}

		public void saveToDevice(string data){
			PlayerPrefs.SetString("level."+getId()+".data", data);
		}
		public string getSavedData(){
			return PlayerPrefs.GetString("level."+getId()+".data", "KOEKJES");
		}

		public bool containsAllBlocks(){
			return getPlacedBlocks().Length == blocks.Length;
		}
		public Block[] getPlacedBlocks(){
			if (grid == null) {
				clear ();
			}
			return placedBlocks.ToArray ();
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
			if (grid == null) {
				clear ();
			}
			if (!placedBlocks.Contains (block)) {
				return;
			}
			placedBlocks.Remove (block);
			for (int y = 0; y < blockHeight; y++) {
				for(int x = 0; x < blockWidth; x++){
					if (grid [x, y] != null && grid [x, y].Equals (block)) {
						grid [x, y] = null;
					}
				}
			}

			block
				.setPos (null)
				.setRotation (0);
			
			hasChanged = true;

		}

		public void printGrid(){
			for (int y = 0; y < blockHeight; y++) {
				string row = "";
				for(int x = 0; x < blockWidth; x++){
					row += getPos (x, y) == null ? "0" : "1"/*getPos (x, y).getId() */;
				}
				Debug.Log (row);
			}
		}

		private Vector2 getLeast(Block block, float deg){
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
			return least;
		}
		private Vector2 getGridPoint(Vector2 col, Vector2 start, Vector2 least, float deg){
			Vector2 temp = start +  VectorCalculation.rotateVector(col, deg) + least;
			return VectorCalculation.revertToOrigin (temp, this);
		}
		public bool setBlock(int x, int y, Block block, float deg=0){
			if (grid == null) {
				clear ();
			}
			hasChanged = true; 

			removeBlock (block);

			Vector2 start = new Vector2 (x, y);
			Vector2 least = getLeast (block, deg);

			foreach(Vector2 col in block.getCollision()){
				Vector2 temp = getGridPoint (col, start, least, deg);
				grid [(int)temp.x, (int)temp.y] = block;
			}

			placedBlocks.Add (block);

			block
				.setPos (start)
				.setRotation(deg);

			return true;
		}


		public bool canSet(int x, int y, Block block, float deg=0){
			Vector2 start = new Vector2 (x, y);
			if (grid == null) {
				clear ();
			}

			Vector2 calc = VectorCalculation.revertToOrigin (start, this);
			if(calc.x >= getWidth() || calc.y >= getHeight()) { 
				return false; 
			}

		
			Vector2 least = getLeast (block, deg);

			foreach(Vector2 col in block.getCollision()){
				Vector2 temp = getGridPoint (col, start, least, deg);
				if(temp.x >= getWidth() || temp.y >= getHeight() || getPos(temp) != null) {
					return false; 
				}
			}

			return true;
		}


		public bool isValidPath(){
			return containsAllBlocks () && getRoad() != null;
		}

		public RoadPiece[] getRoad(){
			if (!hasChanged && road != null) {
				return road;
			}
			hasChanged = false;
			return road = Pathfinding.getInstance ().getRoad (this);
		}

		public int getHeight(){
			return blockHeight;
		}
		public int getWidth(){
			return blockWidth;
		}

	}
}