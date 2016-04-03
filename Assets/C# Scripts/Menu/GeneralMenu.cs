using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Menu
{
	public class GeneralMenu : MonoBehaviour {

		private Button[] buttons;
		private bool isEnabled = true;

		void Start(){
			setButtons ();
			AudioProvider.getInstance ().playBackground ("Menu_Background");
		}

		private void setButtons(){
			Button logo = new Button ("logo");
			Button levelSelect = new Button ("level_select", logo);

			buttons = new Button[] { logo, levelSelect, new Button("credits", levelSelect), PlayerPrefs.GetInt ("audio_mute") == 0 ? new Button("audio_mute") : new Button("audio_unmute")   };
		}

		private float getNumber(float percentage, bool horizontal = true){
			return percentage / 100 * Mathf.Min(Screen.width , Screen.height);
		}

		void OnGUI(){
			GUI.backgroundColor = Color.clear;
			foreach (Button button in buttons) {
				button.calc ();
				if(GUI.Button(new Rect( button.X , button.Y , button.Width, button.Height), button.DefaultImage) && isEnabled){
					switch (button.Id) {
					case "level_select":
						isEnabled = true;
						AudioProvider.getInstance().playAudio("Standard Button");
						SceneManager.LoadScene("Menu_level_select");
						break;
					case "audio_mute":
						PlayerPrefs.SetInt ("audio_mute", 1);
						AudioProvider.getInstance ().stopBackground ();
						setButtons ();
						return;
					case "audio_unmute":
						PlayerPrefs.SetInt ("audio_mute", 0);
						setButtons ();
						AudioProvider.getInstance ().playBackground ();
						break;
					case "credits":
						isEnabled = true;
						AudioProvider.getInstance().playAudio("Standard Button");
						SceneManager.LoadScene("Menu_credits");

						break;
					}

				}
			}


		}

	}
}
