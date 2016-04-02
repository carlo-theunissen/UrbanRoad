using System;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader
{
	private LevelLoader(){}
	private SimpleLevel[] levelCache;
	private static LevelLoader instance;
	public static LevelLoader getInstance(){
		if(instance == null){
			instance = new LevelLoader ();
		}	
		return instance;
	}

	public SimpleLevel[] getLevels(){
		if (levelCache != null) {
			return levelCache;
		}
		int[] ids = getLevelIds ();

		List<SimpleLevel> list = new List<SimpleLevel>();
		foreach (int id in ids) {
			
				list.Add(new SimpleLevel(id));

		}
		return levelCache = list.ToArray();
	}
	public void clearCache(){
		levelCache = null;
	}

	private int[] getLevelIds(){
		
		List<int> list = new List<int> ();
		int index = 1;
		while (true) 
		{
			if(!doesLevelExist(index)){
				break;
			}

			list.Add (index);
			index++;
		}
		return list.ToArray();
	}

	public bool doesLevelExist(int id){
		return Resources.Load ("Config/Levels/level_" + id) != null;
	}
}


