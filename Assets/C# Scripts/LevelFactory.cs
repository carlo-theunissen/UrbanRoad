using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFactory
{
	static LevelFactory instance = null;	
	private LevelFactory() {
	}
	public static LevelFactory getInstance(){
		if (instance == null) {
			instance = new LevelFactory ();
		}
		return instance;
	}

	public Level createLevel(string data, int id){
		string[] rows = data.Split ('\n');
		if (!areRowsValid (rows)) {
			Debug.LogError ("CSV data is not valid");
			return null;
		}
		Level creation = new Level (id);
		setWidthHeight (creation, rows);
		calculate (data, rows [0].Length, creation);
		return creation;

	}
	private bool areRowsValid(string[] rows){
		if (rows.Length == 0) {
			return false;
		}
		int length = rows [0].Length;
		for (int i = 0; i < rows.Length -1; i++) {
			if (length != rows [i].Length) {
				return false;
			}
		}
		return true;
	}
	private void setWidthHeight(Level level, string[] rows){
		level.setHeight (rows.Length);
		level.setWidth (rows[0].Length);
	}
	private void setStart(int width, int loc, Level level){
		level.setStart (getPos (width, loc));
	}
	private void setEnd(int width, int loc, Level level){
		level.setEnd (getPos (width, loc));
	}
	private Vector2 getPos(int width, int pos){
		return new Vector2 ((float) (pos % width),(float) (pos / width));
	}
	private void calculate(string data, int width, Level level){
		Dictionary<int, int> blocks = new Dictionary<int, int>();
		int index = 0;
		foreach(char ch in data.ToCharArray ()){
			if (ch == 'A') {
				setStart (width, index, level);
			}
			if (ch == 'B'){
				setEnd(width, index, level);
			}
			if ((ch >= '1' && ch <= '9')) {
				int id = int.Parse (ch.ToString ());
				int val = 1;

				if (blocks.ContainsKey (id)) {
					blocks.TryGetValue (id, out val);
					val++;
					blocks.Remove (id);
				}
				blocks.Add (id, val);
			}
			index++;
		}

		calculateBlocks (blocks, level);
	}
	private void calculateBlocks(Dictionary<int, int> blocks, Level level){
		List<Block> block = new List<Block> ();
		PieceFactory factory  = PieceFactory.getInstance ();
		foreach(KeyValuePair<int, int> entry in blocks)
		{
			PieceConfig config = factory.getConfig (entry.Key);
			if (config == null) {
				throw new System.InvalidOperationException ("Piece is null");
			}
		
			for (int i = 0; i < (entry.Value / config.blockCount ); i++) {
				block.Add (makeBlock (config));
			}
		}

		level.setBlocks (block.ToArray ());

	}
	private Block makeBlock(PieceConfig config){
		return Block.createFromConfig (config);
	}
}


