using UnityEngine;
using System.Collections;
namespace Game
{
	public class GameMode
	{
		private static Level currentLevel; 
		private GameMode(){} 

		private static Level getLevel(int id){
			string data = getLevelData (id);
			return LevelFactory.getInstance ().createLevel (data, id);
		}
		private static string getLevelData(int id){
			TextAsset level = (TextAsset)Resources.Load ("Config/Levels/level_" + id);
			return level.text;
		}
		public static Level getCurrentLevel(){
			if (currentLevel == null) {
				currentLevel = getLevel (2);
			}
			return currentLevel;
		}
	}

}
