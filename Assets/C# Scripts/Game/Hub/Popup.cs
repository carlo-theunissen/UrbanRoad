using System;
using UnityEngine;
using System.Collections;

namespace Game
{
	public class Popup : MonoBehaviour
	{

		public float frames = 1000;
		public float change = 1000;
		public float delaySec = .1f;
		private bool show = false;
		private bool animating = false;
		public void Display(){
			if (show) {
				return;
			}
		
			gameObject.SetActive (true);
			StartCoroutine (this.Show ());
		}
		public bool isAnimating(){
			return animating;
		}
		public bool isShown(){
			return show;
		}
		public void OutAnimation(){
			if (!show) {
				return;
			}
			gameObject.SetActive (true);
			StartCoroutine (this.Clear ());
		}
		private float bounce (float t, float b, float c, float d){
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
		private float ease(float t, float b, float c, float d){
			t /= d/2;
			if (t < 1) return c/2*t*t*t*t*t + b;
			t -= 2;
			return c/2*(t*t*t*t*t + 2) + b;
		}
		private IEnumerator Show(){
			
			int tick = 0;
			animating = true;
			while(tick < frames){
				if (tick == 1) {
					yield return new WaitForSeconds (delaySec);
				}
				tick++;
				float calc = bounce(tick,0,1,frames) * change;
 
				Transform child = this.gameObject.transform.GetChild(0);

				RectTransform trans = child as RectTransform;
				Vector3 pos = trans.localPosition;
				pos.y = (change - calc) * 1;

				trans.localPosition = pos;
				yield return null;
			}
			animating = false;
			show = true;

		}
		private IEnumerator Clear(){
			int tick = 0;
			animating = true;
			while (tick < frames) {
				tick++;
				float calc = ease(tick,0,1,frames) * change;

				Transform child = this.gameObject.transform.GetChild(0);

				RectTransform trans = child as RectTransform;
				Vector3 pos = trans.localPosition;
				pos.y = (calc)  * -1;

				trans.localPosition = pos;
				yield return null;
			}
			animating = false;
			show = false;
		}
	}
}

