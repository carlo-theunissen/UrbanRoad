using System;
using UnityEngine;

public class BlockPlacer: MonoBehaviour {
	public Grid grid;
	private Level level;

	public BlockPlacer(){
		level = GameMode.getCurrentLevel ();
	}
	public void Start(){
		loadFromDevice ();
	}
	public void hover(Block block, Vector2 pos, float deg){
	
		GameObject prefab = block.getBlueprintPrefab ();
		Vector2 temp = transformToGrid (pos, block.getWidthHeight (deg));

		Renderer rend = prefab.GetComponent<Renderer> ();
		rend.material.shader = Shader.Find ("Specular");
		rend.material.SetColor ("_SpecColor", canPlacePiece (pos, block, deg) ? Color.white : Color.red);
		grid.placeDummy (temp.x, temp.y, prefab, deg);
	}

	public void placeObject(Block block, Vector2 pos, float deg, bool wasDummy = true){
		if (canPlacePiece (pos, block, deg)) {
			level.setBlock ((int) pos.x, (int) pos.y, block, deg);
			if (!wasDummy) {
				pos = transformToGrid (pos, block.getWidthHeight (deg));
				grid.placeObject (pos.x, pos.y, block.getBlueprintPrefab (), deg);
			} else {
				AudioPlayer("build");
				saveToDevice ();
			}
			drawRoad ();

		} else {
			level.removeBlock (block);
			block.removeBlueprintPrefab ();
			AudioPlayer("error");
		}
	}
	public void clearBlocks(bool keepStoredData = false){
		foreach (Block block in level.getBlocks()) {
			block.setPos (null).setRotation (0);
			block.removeBlueprintPrefab ();
		}
		level.clear ();
		if (!keepStoredData) {
			level.saveToDevice ("");
		}
	}
	private void saveToDevice(){
		level.saveToDevice (serializeBlocks ());
	}
	public void loadFromDevice(){
		clearBlocks (true);
		loadFromData (level.getSavedData ());
	}
	private void loadFromData(string data){
		foreach(string line in data.Split('|')){
			if (line != "") {
				
				string[] calc = line.Split (':');
				foreach (Block block in level.getBlocks()) {
					if (block.getId () == int.Parse( calc [0] ) && block.getPos () == null) {
						
						block.unserialize (calc [1]);
						placeObject (block, (Vector2) block.getPos (), block.getRotation (), false);
						break;
					}
				}
			}
		}
	}
	private string serializeBlocks(){
		string calc = "";
		foreach (Block block in level.getBlocks()) {
			if (block.getPos () != null) {
				calc += "|"+block.getId()+":"+block.serialize()+"|";
			}
		}
		return calc;
	}
	private void drawRoad(){
		if (level.containsAllBlocks ()) {
			RoadPiece[] pieces = level.getRoad ();
			if (pieces != null) {
				foreach (RoadPiece road in pieces) {
					Vector2 pos = VectorCalculation.revertToOrigin (road.Position, level);
					grid.placeRoad (pos.x, pos.y, road.getPrefab ());
				}
			}
		}
	}



	/**
	 * Verkrijgt de linker boven hoek in pos en vertaald dat naar een vector zodat het object goed staat
	 */ 
	private Vector2 transformToGrid(Vector2 pos, Vector2 dimensions){ 
		return pos + new Vector2 (dimensions.x  / 2 - 0.5f, dimensions.y / 2 - 0.5f);

	}
	private bool canPlacePiece(Vector2 pos, Block piece, float deg){

		Vector2 dimensions = piece.getWidthHeight (deg);

		if (pos.x < 0 || pos.y < 0 || pos.x + dimensions.x  > level.getWidth () || pos.y + dimensions.y > level.getHeight() ) {
			return false;
		}
		level.removeBlock (piece);
		return level.canSet((int) pos.x,(int) pos.y,piece,deg);
	}


	public void AudioPlayer(string audioBuild)
	{

		AudioSource source = gameObject.GetComponent<AudioSource>();
		AudioProvider.getInstance().playAudio(audioBuild,source);

	}



}

