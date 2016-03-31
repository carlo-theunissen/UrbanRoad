using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Game
{
	public class RotationButton : MonoBehaviour,IPointerClickHandler {
		public Mouse mouse;
		private bool rotating = false;
		public void OnPointerClick(PointerEventData eventData){
			rotate ();
		}
		private void rotate(){
			int deg = mouse.getDeg ();
			deg += 90;
			deg %= 360;
			mouse.setDeg (deg);
            AudioProvider.getInstance().playAudio("Standard Button");
        }
		void Update(){
			if (Input.GetKey (KeyCode.R)) {
				if (!rotating) {
                    rotate ();
				}
				rotating = true;
			} else {
				rotating = false;
			}
		}
	}
}