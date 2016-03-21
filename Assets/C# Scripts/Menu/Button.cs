using System;
using UnityEngine;
using System.Xml;

namespace Menu
{
	public class Button
	{
		private Texture2D defaultImage;
		private Texture2D clickedImage;
		private float x;
		private float y;
		private float width;
		private float height;
		private string id;
		public Button (string id)
		{
			this.id = id;
			XmlDocument xml = XmlHelper.getXml("menu");
			XmlNode node = xml.SelectSingleNode ("//button[@id = '"+id+"']");

			defaultImage = getTexture (ref node, "default");
			clickedImage = getTexture (ref node, "clicked");
			x = float.Parse(node.Attributes ["x"].Value);
			y = float.Parse(node.Attributes ["y"].Value);
			width =  float.Parse(node.Attributes ["width"].Value);
			height = width / (defaultImage.width / defaultImage.height );
		}

		private Texture2D getTexture(ref XmlNode node, string name){
			XmlNode check = node.SelectSingleNode ("//img[@type = '"+name+"']");
			string fileName = check.Attributes ["src"].Value;
			return (Texture2D) Resources.Load ("Menu/"+fileName);
		}



		public Texture2D DefaultImage {
			get {
				return this.defaultImage;
			}
		}

		public Texture2D ClickedImage {
			get {
				return this.clickedImage;
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

