using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
	class Button : MonoBehaviour, IPointerDownHandler
    {
        public GameObject prefab;
		private UnityEngine.UI.Button counter;
		private GameObject icon;
		private Mouse mouse;
        public Vector2 dimensions;
        
        public Block[] blocks;
		private int lastUnused = -1;

		public void OnPointerDown(PointerEventData eventData){
			Block temp = getAviableBlock ();
			if (temp != null) {
				mouse.setDeg (0);
				mouse.setPiece ((Block)temp);
				mouse.setFollowing (true);
				AudioProvider.getInstance().playAudio("Standard Button");
			}
		}
		void Update(){
			calculateTotal ();
		}
        private void Start(){
            
			mouse = GameObject.Find ("Mouse").GetComponent<Mouse>();
				
            UnityEngine.UI.Button picButton = this.GetComponent< UnityEngine.UI.Button >();
            picButton.image.sprite = blocks[0].getPlaceholder();


			calculateTotal ();
            
            
            
        }
		private int getUnused(){
			int calc = 0;
			foreach (Block block in blocks) {
				if (block.getPos () == null) {
					calc++;
				}
			}
			return calc;
		}
		private Block getAviableBlock(){
			foreach (Block block in blocks) {
				if (block.getPos () == null) {
					return block;
				}
			}
			return null;
		}

		private void calculateTotal(){
			int unused = getUnused ();
			if (lastUnused == unused) {
				return;
			}
			lastUnused = unused;

			if (unused > 1) {
				if (counter == null || !counter.IsActive()) {
					createCounter ();
				}
				((UnityEngine.UI.Button)counter).image.sprite = getNumberIcon (unused);
			} else {
				UnityEngine.Object.Destroy (icon);
			}

			Image image = GetComponent<Image>();
			Color c = image.color;
			c.a = unused == 0? .5f : 1;
			image.color = c;
		}
		private void createCounter(){
			icon = getNumberButton(this.transform);
			counter = icon.GetComponent< UnityEngine.UI.Button >();
			calculateTotal ();
		}
		private GameObject getNumberButton(Transform parent){
            GameObject icon = UnityEngine.Object.Instantiate(prefab);
			icon.transform.localPosition = new Vector3(0,0, 0);

			icon.transform.SetParent(this.transform);

			Vector2 temp = dimensions;
			temp.y = -temp.y;
			icon.transform.localPosition = temp;


	        return icon;
        }
        
		private Sprite getNumberIcon(int total){
			return Resources.Load ("GameHub/numbers/Number_" + total, typeof(Sprite)) as Sprite;
		}
    }
}