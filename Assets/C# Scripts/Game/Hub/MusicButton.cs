using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game
{
	public class MusicButton : MonoBehaviour,IPointerClickHandler {
		public Sprite mute;
		public Sprite unMute;
		public void OnPointerClick(PointerEventData eventData){
			if (PlayerPrefs.GetInt ("audio_mute") != 1) {
				PlayerPrefs.SetInt ("audio_mute", 1);
				AudioProvider.getInstance ().stopBackground ();
			} else {
				PlayerPrefs.SetInt ("audio_mute", 0);
				AudioProvider.getInstance ().playBackground ();
			}
			setImage ();
		}

		public void Start(){
			setImage ();
		}
		private void setImage(){
			UnityEngine.UI.Button picButton = this.GetComponent< UnityEngine.UI.Button >();
			if (PlayerPrefs.GetInt ("audio_mute") != 1) {
				picButton.image.sprite = mute;
			} else {
				picButton.image.sprite = unMute;
			}
		}
	}
}