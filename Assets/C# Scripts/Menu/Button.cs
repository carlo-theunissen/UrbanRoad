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

		private float maxX;
		private float maxY; 
		private float minWidth;
		private float minHeight;

		private string id;

		private float calculatePercentage(string percentage){
			percentage = percentage.Replace ("%", "");
			return float.Parse (percentage);
		}
		private float calculatePixels(string pixels){
			pixels = pixels.Replace ("px", "");
			return float.Parse (pixels);
		}

		public Button (string id)
		{
			this.id = id;
			XmlDocument xml = XmlHelper.getXml("menu");
			XmlNode node = xml.SelectSingleNode ("//button[@id = '"+id+"']");

			defaultImage = getTexture (ref node, "default");
			clickedImage = getTexture (ref node, "clicked");
			x = calculatePercentage(node.Attributes ["x"].Value);
			y = calculatePercentage(node.Attributes ["y"].Value);
			width =  calculatePercentage(node.Attributes ["width"].Value);
			height = width / (defaultImage.width / defaultImage.height );

			maxX = calculatePixels (node.Attributes ["max-x"].Value);
			maxY = calculatePixels (node.Attributes ["max-y"].Value);
			minWidth = calculatePixels (node.Attributes ["min-width"].Value);
			minHeight = minWidth / (defaultImage.width / defaultImage.height );
		}

		private Texture2D getTexture(ref XmlNode node, string name){
			XmlNode check = node.SelectSingleNode ("//img[@type = '"+name+"']");
			string fileName = check.Attributes ["src"].Value;
			return (Texture2D) Resources.Load ("Menu/"+fileName);
		}


		private float getNumber(float percentage){
			return percentage / 100 * Mathf.Min(Screen.width , Screen.height);
		}

		public float getX(){
			return Mathf.Min (maxX, getNumber (x));
		}

		public float getY(){
			return Mathf.Min (maxY, getNumber (y));
		}

		public float getWidth(){
			return Mathf.Max (minWidth, getNumber (width));
		}

		public float getHeight(){
			return Mathf.Max (minHeight, getNumber (height));
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
		public float MaxX {
			get {
				return this.maxX;
			}
		}

		public float MaxY {
			get {
				return this.maxY;
			}
		}

		public float MinWidth {
			get {
				return this.minWidth;
			}
		}
	}
}

