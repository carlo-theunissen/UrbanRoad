using System;
using UnityEngine;

public class GUIcalculation
{
	private static Vector2[] guiPos;
	public static void setGuiPos(){
		guiPos = new Vector2[Input.touchCount];
		Vector2 screenVector = new Vector2 (0, Screen.height);

		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.GetTouch (i);
				guiPos [i] = touch.position - screenVector;
			}
		} else if (Input.GetMouseButton (0)) { 
			guiPos = new Vector2[1];
			Vector2 temp = Input.mousePosition;
			temp.y = Mathf.Abs(temp.y -  Screen.height);

			guiPos [0] = temp;
		}
	}


	public static bool collisionWithTouch(Rect rect){
		if (rect.height == 0 || rect.width == 0) {
			return false;
		}

		foreach (Vector2 pos in guiPos) {
			if ( rect.Contains(pos)) {
				return true;
			}
		}
		return false;
	}
}


