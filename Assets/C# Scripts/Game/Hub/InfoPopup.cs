using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Game 
{
	public class InfoPopup : Popup, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData){
			OutAnimation ();
		}
	}
}

