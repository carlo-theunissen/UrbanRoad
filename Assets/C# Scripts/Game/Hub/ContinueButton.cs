using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Game
{
	public class ContinueButton : MonoBehaviour,IPointerClickHandler {
		public void OnPointerClick(PointerEventData eventData){
			this.transform.parent.transform.parent.GetComponent<Popup>().OutAnimation();
			GameMode.getCurrentLevel ().setLocked (false);
			AudioProvider.getInstance().playAudio("Standard Button");
		}
	}
}