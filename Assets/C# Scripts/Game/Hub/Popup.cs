using System;
using UnityEngine;
using System.Collections;

namespace Game
{
	public class Popup : MonoBehaviour
	{
		private bool up = true;
		public float frames = 1000;
		public float change = 1000;
		private float tick = 0;

		public void Display(){
			tick = 0;
			up = true;
			//gameObject.SetActive (true);
			//StartCoroutine (this.Tick ());
		}
		private float easing (float t, float b, float c, float d){
			if ((t/=d) < (1/2.75f)) {  
				return c*(7.5625f*t*t) + b;  
			} else if (t < (2f/2.75f)) {  
				return c*(7.5625f*(t-=(1.5f/2.75f))*t + .75f) + b;  
			} else if (t < (2.5f/2.75f)) {  
				return c*(7.5625f*(t-=(2.25f/2.75f))*t + .9375f) + b;  
			} else {  
				return c*(7.5625f*(t-=(2.625f/2.75f))*t + .984375f) + b;  
			}  
		}
		private IEnumerator Tick(){
			tick++;
			float calc = easing(0,1,frames,tick) * change;
			Transform child = this.gameObject.transform.GetChild(0);

			RectTransform trans = child as RectTransform;
			Vector3 pos = trans.localPosition;
			pos.y = (up ? change : 0) - calc;

			trans.localPosition = pos;
			if (tick < frames) {
				yield return null;
			}

		}
	}
}

