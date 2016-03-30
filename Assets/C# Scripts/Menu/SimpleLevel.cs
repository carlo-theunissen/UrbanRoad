using System;
using UnityEngine;
namespace Menu
{
	public class SimpleLevel
	{
		private Sprite image;
		public int id;
		public SimpleLevel (int id)
		{
			this.id = id;
		}
		public bool isCompleted(){
			return PlayerPrefs.GetInt ("completed_" + id, 0) == 1;
		}
		public Sprite getImage(){
			if (image != null) {
				return image;
			}

			return image = Resources.Load("Menu/LevelSelect/"+ (isCompleted() ? "complete" : "uncompleted") +"/"+id, typeof(Sprite)) as Sprite;

		}
	}
}

