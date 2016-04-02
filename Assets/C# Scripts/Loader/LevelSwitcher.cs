using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSwitcher
{
	public static int levelToLoad = 1;
	public static void loadLevel(int id){
		levelToLoad = id;
		SceneManager.LoadScene ("LoadingScreen");
	}
}


