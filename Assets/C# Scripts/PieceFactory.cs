using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class PieceFactory
{
	static PieceFactory instance = null;
	private XmlDocument xml;
	private PieceFactory() {
		xml = new XmlDocument ();
		xml.LoadXml (getPieceData ());
	}
	public static PieceFactory getInstance(){
		if (instance == null) {
			instance = new PieceFactory ();
		}
		return instance;
	}

	public PieceConfig getConfig(int id){
		PieceConfig config = new PieceConfig ();
		config.id = id;
		XmlNode node = xml.SelectSingleNode ("//piece[@id = '"+id+"']");
		if (node == null) {
			Debug.LogError ("Id:"+id + " piece not found!");
		}
		XmlNodeList blocks = node.SelectNodes ("blocks/block");
		config.collision = getCollision (blocks);
		config.blockCount = blocks.Count;
		config.prefab = getPrefab (node.SelectSingleNode ("prefab"));

		return config;
	}

	private GameObject getPrefab(XmlNode node){

		string name = node.Attributes ["name"].Value;
		return (GameObject)Resources.Load ("Prefabs/"+name);
	}

	private List<Vector2> getCollision(XmlNodeList list){
		List<Vector2> output = new List<Vector2>();
		foreach (XmlNode node in list) {
			Vector2 temp = new Vector2 ();
			temp.x = int.Parse( node.Attributes["x"].Value );
			temp.y = int.Parse( node.Attributes["y"].Value );
			output.Add (temp);
		}
		return output;
	}

	private static string getPieceData(){
		TextAsset level = (TextAsset)Resources.Load ("Config/pieces");
		return level.text;
	}
}


