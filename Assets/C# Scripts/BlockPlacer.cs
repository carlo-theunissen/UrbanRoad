using System;
using UnityEngine;

public class BlockPlacer: MonoBehaviour {
	public Grid grid;
	private Level level;

	public BlockPlacer(){
		level = GameMode.getCurrentLevel ();
	}

	public void hover(Block block, Vector2 pos, float deg){
		
		GameObject prefab = block.getBlueprintPrefab ();
		Vector2 temp = transformToGrid (pos, block.getWidthHeight (deg));

		Renderer rend = prefab.GetComponent<Renderer> ();
		rend.material.shader = Shader.Find ("Specular");
		rend.material.SetColor ("_SpecColor", canPlacePiece (pos, block, deg) ? Color.white : Color.red);
		grid.placeDummy (temp.x, temp.y, prefab, deg);
	}

	public void placeObject(Block block, Vector2 pos, float deg){
		if (canPlacePiece (pos, block, deg)) {
			GameMode.getCurrentLevel ().setBlock ((int) pos.x, (int) pos.y, block, deg);
			drawRoad ();
			AudioPlayer("build");

		} else {
			level.removeBlock (block);
			block.removeBlueprintPrefab ();
			AudioPlayer("error");
		}
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


	private void AudioPlayer(string audioBuild)
	{

		AudioSource source = gameObject.GetComponent<AudioSource>();
		AudioProvider.getInstance().playAudio(audioBuild,source);

	}



}

