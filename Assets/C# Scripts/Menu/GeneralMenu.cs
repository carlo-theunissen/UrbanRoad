using UnityEngine;
using System.Collections;

namespace Menu
{
	public class GeneralMenu : MonoBehaviour {

		private Button[] buttons;
		private bool isEnabled = true;
		private AudioSource source;

		void Start(){
			setButtons ();
			source = gameObject.GetComponent<AudioSource>();
			AudioProvider.getInstance ().playAudio ("build", source, true);
			if (PlayerPrefs.GetInt ("audio_mute") == 1) {
				source.Stop ();
			}
		}

		private void setButtons(){
			buttons = new Button[] { new Button("level_select"), PlayerPrefs.GetInt ("audio_mute") == 0 ? new Button("audio_mute") : new Button("audio_unmute"), new Button("credits")   };
		}

		private float getNumber(float percentage, bool horizontal = true){
			return percentage / 100 * Mathf.Min(Screen.width , Screen.height);
		}

		private void playAudio(string name, bool isBackground){
			
			AudioProvider.getInstance().playAudio(name,source, isBackground);
		}

		void OnGUI(){
			
			foreach (Button button in buttons) {
				if(GUI.Button(new Rect( button.getX() , button.getY() , button.getWidth(), button.getHeight()), button.DefaultImage) && isEnabled){
					switch (button.Id) {
					case "level_select":
						isEnabled = true;
						Application.LoadLevel(1);
						break;
					case "audio_mute":
						PlayerPrefs.SetInt ("audio_mute", 1);
						source.Stop ();
						setButtons ();
						return;
					case "audio_unmute":
						PlayerPrefs.SetInt ("audio_mute", 0);
						setButtons ();
						source.Play ();
						break;
					case "credits":
						isEnabled = true;
						Application.LoadLevel(2);
						break;
					}

				}
			}


		}

	}
}
