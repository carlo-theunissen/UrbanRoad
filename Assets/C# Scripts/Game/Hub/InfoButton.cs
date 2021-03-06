﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


namespace Game
{
	public class InfoButton : MonoBehaviour,IPointerClickHandler {
		public Popup infoButton;
		private bool tried = false;
		public void OnPointerClick(PointerEventData eventData){
			AudioProvider.getInstance().playAudio("Standard Button");
			if (infoButton.isShown ()) {
				infoButton.OutAnimation ();
			} else {
				infoButton.Display ();
			}
		}
		void LateUpdate(){
			if (!tried) {
				tried = true;
				if (GameMode.getCurrentLevel ().getId () == 1 && !GameMode.getCurrentLevel ().isLocked ()) {
					infoButton.Display ();
				}
			}
		}
	}
}

