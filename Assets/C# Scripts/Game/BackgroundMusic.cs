using System;
using UnityEngine;

namespace Game
{
	public class BackgroundMusic : MonoBehaviour
	{
		void Start(){
			
			AudioProvider.getInstance ().playBackground ("BG music");
		}
	}
}

