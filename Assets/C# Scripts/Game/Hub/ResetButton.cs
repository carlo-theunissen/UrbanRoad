using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game
{
	public class ResetButton : MonoBehaviour,IPointerClickHandler {
		public BlockPlacer placer;
		public void OnPointerClick(PointerEventData eventData){
            AudioProvider.getInstance().playAudio("Reset");
            placer.clearBlocks ();
			GameMode.getCurrentLevel().storeCompleteStatus ();
		}
	}
}