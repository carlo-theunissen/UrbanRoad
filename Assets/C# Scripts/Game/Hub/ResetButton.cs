using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game
{
	public class ResetButton : MonoBehaviour,IPointerClickHandler {
		public BlockPlacer placer;
		public void OnPointerClick(PointerEventData eventData){
			placer.clearBlocks ();
		}
	}
}