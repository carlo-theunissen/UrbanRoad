using System;
using System.Xml;
using UnityEngine;

public class XmlHelper
{
	private static XmlDocument doc;
	private static string storedFile;
	public static XmlDocument getXml(string file="game"){
		if (storedFile != file || doc == null) {
			storedFile = file;
			doc = new XmlDocument ();
			doc.LoadXml (getPieceData (file)); 
		}
		return doc;
	}
	private static string getPieceData(string file){
		TextAsset level = (TextAsset)Resources.Load ("Config/"+file);
		return level.text;
	}
}


