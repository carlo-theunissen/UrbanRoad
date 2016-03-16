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
		calculate (data,  creation);
		return creation;

	}
	private bool areRowsValid(string[] rows){
		if (rows.Length == 0) {
			return false;
		}
        int length = rows[0].Split(',').Length;
		for (int i = 0; i < rows.Length -1; i++) {
            string[] temp = rows[i].Split(',');
			if (length != temp.Length) {
				return false;
			}
		}
		return true;
	}
	private void setWidthHeight(Level level, string[] rows){
		int length = rows [rows.Length - 1] == "" ? rows.Length - 1 : rows.Length;
		level.setHeight (length);
		level.setWidth (rows[0].Replace(",","").Length -1);
	}
	private void setStart(int loc, Level level){
		level.setStart (getPos (level.getWidth(), level.getHeight(), loc));
	}
	private void setEnd(int loc, Level level){
		level.setEnd (getPos (level.getWidth(), level.getHeight(), loc));
	}
	private Vector2 getPos(int width, int height, int pos){
		return new Vector2 ((float) (pos % width) ,(float) (pos / width));
	}
	private void calculate(string data, Level level){
		Dictionary<int, int> blocks = new Dictionary<int, int>();
		int index = 0;
		foreach(char ch in data.ToCharArray ()){
			if (!(ch == 'A' || ch == 'B' ||  (ch >= '0' && ch <= '9' ))) {
				continue;
			}

			if (ch == 'A') {
				setStart (index, level);
			}
			if (ch == 'B'){
				setEnd(index, level);
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


