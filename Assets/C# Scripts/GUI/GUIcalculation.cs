using System;
using UnityEngine;

public class CustomGUI
{
	private static Vector2[] guiPos;
	public static void setGuiPos(){
		guiPos = new Vector2[Input.touchCount];
		Vector2 screenVector = new Vector2 (0, Screen.height);

		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.GetTouch (i);
				guiPos[i] = touch.position - screenVector;
			}
		}
	}

	public static bool collisionWithTouch(Rect rect){
		if (rect.height == 0 || rect.width == 0) {
			return false;
		}
		foreach (Vector2 pos in guiPos) {
			if ( 
				rect.x >= pos.x && rect.x + rect.width < pos.x &&
				rect.y >= pos.y && rect.y + rect.height < pos.y) {
				return true;
			}
		}
		return false;
	}
}


