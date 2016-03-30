using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSwitcher
{
	public static int levelToLoad;
	public static void loadLevel(int id){
		levelToLoad = id;
		SceneManager.LoadScene ("LoadingScreen");
	}
}


