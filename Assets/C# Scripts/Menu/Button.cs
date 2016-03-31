using System;
using UnityEngine;
using System.Xml;

namespace Menu
{
	public class Button
	{
		private Texture2D defaultImage;
		private float x;
		private float y;
		private float width;
		private float height;

		private Button parent;
		private string id;



		public void calc(){
			switch (id) {
			case "audio_mute":
			case "audio_unmute":
				x = 10;
				width = Mathf.Max(getNumber (10), 50);
				height = width / (defaultImage.width / defaultImage.height);
				y = Screen.height - height - 10;
				break;
			case "logo":
				x = 10;
				y = 40;
				width = Mathf.Max(getNumber (70), 300);
				break;
			case "level_select":
			case "credits":
				x = 10;
				y = parent.Y + parent.Height + 10;
				width = Mathf.Max(getNumber (50), 200);
				break;
			}	
			 
			height = width / (defaultImage.width / defaultImage.height);
		}

		public Button (string id)
		{
			
			this.id = id;
			calculateDefaultImage ();

		}
		public Button(string id, Button parent){
			this.parent = parent;
			this.id = id;
			calculateDefaultImage ();
		}
		private void calculateDefaultImage(){
			XmlDocument xml = XmlHelper.getXml("menu");
			XmlNode node = xml.SelectSingleNode ("//button[@id = '"+id+"']");
			defaultImage = getTexture (ref node, "default");
		}

		private Texture2D getTexture(ref XmlNode node, string name){
			XmlNode check = node.SelectSingleNode ("img[@type = '"+name+"']");
			string fileName = check.Attributes ["src"].Value;

			return (Texture2D) Resources.Load ("Menu/"+fileName);
		}


		private float getNumber(float percentage){
			return percentage / 100 * Mathf.Min(Screen.width , Screen.height);
		}

		public Texture2D DefaultImage {
			get {
				return this.defaultImage;
			}
		}
			

		public float X {
			get {
				return this.x;
			}
		}

		public float Y {
			get {
				return this.y;
			}
		}

		public float Width {
			get {
				return this.width;
			}
		}
		public float Height {
			get {
				return this.height;
			}
		}
		public string Id {
			get {
				return this.id;
			}
		}
	}
}

