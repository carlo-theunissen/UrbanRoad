using UnityEngine;
using System.Collections;

public class GameMode
{
	private GameMode(){} 

	public static Level getLevel(int id){
		string data = getLevelData (id);
		return LevelFactory.getInstance ().createLevel (data, id);
	}
	private static string getLevelData(int id){
		TextAsset level = (TextAsset)Resources.Load ("Config/Levels/level_" + id);
		return level.text;
	}
}


