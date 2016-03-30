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
<<<<<<< HEAD
				currentLevel = getLevel (20);
=======
				currentLevel = getLevel (5);
>>>>>>> d0f9ff84fda3f5d3542a18ce95fedde628e58f8f
			}
			return currentLevel;
		}
	}

}
