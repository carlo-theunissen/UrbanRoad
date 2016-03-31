using System;
using UnityEngine;
public class SimpleLevel
{
	private Sprite image;
	private Sprite downImage;
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
	public Sprite getDownImage(){
		if (isCompleted ()) {
			return getImage ();
		}
		if (downImage != null) {
			return image;
		}
		return downImage = Resources.Load("Menu/LevelSelect/complete/"+id, typeof(Sprite)) as Sprite;

	}
	public void setCompletedStatus(bool completed){
		PlayerPrefs.SetInt ("completed_" + id, (completed?1:0));
	}
}

