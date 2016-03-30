using System;
using UnityEngine;
namespace Game
{
	public class CanvasSizer : MonoBehaviour
	{
		public GameObject parent;

		void Start(){
			RectTransform t = parent.transform as RectTransform;
			RectTransform current = this.transform as RectTransform;
			Vector2 size = current.sizeDelta;
			size.y = t.rect.height;
			current.sizeDelta = size;
		}
	}
}

