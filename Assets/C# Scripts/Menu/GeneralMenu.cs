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
			Button logo = new Button ("logo");
			Button levelSelect = new Button ("level_select", logo);

			buttons = new Button[] { logo, levelSelect, new Button("credits", levelSelect), PlayerPrefs.GetInt ("audio_mute") == 0 ? new Button("audio_mute") : new Button("audio_unmute")   };
		}

		private float getNumber(float percentage, bool horizontal = true){
			return percentage / 100 * Mathf.Min(Screen.width , Screen.height);
		}

		private void playAudio(string name, bool isBackground){
			
			AudioProvider.getInstance().playAudio(name,source, isBackground);
		}

		void OnGUI(){
			GUI.backgroundColor = Color.clear;
			foreach (Button button in buttons) {
				button.calc ();
				if(GUI.Button(new Rect( button.X , button.Y , button.Width, button.Height), button.DefaultImage) && isEnabled){
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
