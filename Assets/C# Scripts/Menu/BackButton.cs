using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Menu
{
	public class BackButton : MonoBehaviour,IPointerClickHandler {
		public void OnPointerClick(PointerEventData eventData){
			AudioProvider.getInstance().playAudio("Standard Button");
			SceneManager.LoadScene ("Menu_start");
		}
	}
}