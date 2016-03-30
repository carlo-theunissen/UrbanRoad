using System;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace Menu
{
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
				if (id != null) {
					list.Add(new SimpleLevel(id));
				}
			}
			return levelCache = list.ToArray();
		}

		private int[] getLevelIds(){
			DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Config/Levels");
			FileInfo[] info = dir.GetFiles("*.csv");
			int[] ids = new int[info.Length];
			int index = 0;
			foreach (FileInfo f in info) 
			{
				Regex regex = new Regex(@"level_([0-9]+)\.csv$");
				Match match = regex.Match(f.FullName);
				if (match.Success)
				{
					ids [index++] = int.Parse (match.Groups [1].Value);
				}
			}
			Array.Sort (ids);
			return ids;
		}
	}
}

