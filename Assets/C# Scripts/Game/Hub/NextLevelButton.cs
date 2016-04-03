using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game
{
	public class NextLevelButton : MonoBehaviour,IPointerClickHandler {

		private Level level;
		private bool active = true;
		void Start(){
			level = GameMode.getCurrentLevel ();
			if (!LevelLoader.getInstance ().doesLevelExist (level.getId () + 1)) {
				active = false;
				this.gameObject.SetActive(false);
			}

		}
		public void OnPointerClick(PointerEventData eventData){
			if (active) {
				LevelSwitcher.loadLevel (level.getId () + 1);
				AudioProvider.getInstance().playAudio("Standard Button");
			}
		}
	}
}

