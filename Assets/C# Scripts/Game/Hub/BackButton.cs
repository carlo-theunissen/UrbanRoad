using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Game
{
	public class BackButton : MonoBehaviour,IPointerClickHandler {
		public void OnPointerClick(PointerEventData eventData){
			SceneManager.LoadScene("Menu_start");
		}
	}
}