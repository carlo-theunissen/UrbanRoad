using System;
using UnityEngine;
using UnityUi = UnityEngine.UI;
namespace Menu
{
	public class ButtonPlacer : MonoBehaviour
	{
		public GameObject prefab;
		public GameObject parent;
		void Start(){
			foreach (SimpleLevel level in LevelLoader.getInstance().getLevels()) {
				GameObject work = Instantiate (prefab);
				work.transform.SetParent( parent.transform,false );
				UnityUi.Button but = work.GetComponent<UnityUi.Button> ();
				but.image.sprite = level.getImage ();


				LevelSelectButton selectButton = work.AddComponent<LevelSelectButton> ();
				selectButton.id = level.id;
				selectButton.down = level.getDownImage ();
			}
		}
		void OnDestroy(){
			LevelLoader.getInstance ().clearCache ();
		}
	}
}

