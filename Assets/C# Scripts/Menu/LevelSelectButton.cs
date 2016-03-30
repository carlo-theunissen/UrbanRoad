using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Menu
{
	public class LevelSelectButton: MonoBehaviour, IPointerClickHandler {

		public int id;
		public void OnPointerClick(PointerEventData eventData){
			LevelSwitcher.loadLevel (id);
		}


	}
}

