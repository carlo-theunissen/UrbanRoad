using System;
using System.Xml;
using UnityEngine;

public class XmlHelper
{
	private static XmlDocument doc;
	public static XmlDocument getXml(){
		if (doc == null) {
			doc = new XmlDocument ();
			doc.LoadXml (getPieceData ()); 
		}
		return doc;
	}
	private static string getPieceData(){
		TextAsset level = (TextAsset)Resources.Load ("Config/pieces");
		return level.text;
	}
}


