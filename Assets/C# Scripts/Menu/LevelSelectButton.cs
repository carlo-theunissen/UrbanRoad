using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUi = UnityEngine.UI;

namespace Menu
{
	public class LevelSelectButton: MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {

		public int id;
		public Sprite down;
		private Sprite up;
		private UnityUi.Button button;

		void Start(){
			this.button = GetComponent<UnityUi.Button> ();
			up = this.button.image.sprite;
		}

		public void OnPointerClick(PointerEventData eventData){
			LevelSwitcher.loadLevel (id);
		}

		public void OnPointerDown(PointerEventData eventData){
			button.image.sprite = down;
		}

		public void OnPointerUp(PointerEventData eventData){
			button.image.sprite = up;
		}


	}
}

