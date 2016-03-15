using System;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class RoadPieceHelper
{
	private static Dictionary<int,GameObject> cache;

	private static void makeCache(){
		if(cache == null){
			cache = new Dictionary<int,GameObject>();
		}
	}

	public static GameObject getRoadPrefab(int id){
		makeCache ();

		GameObject gameObj = getCached (id);
		if (gameObj != null) {
			return gameObj;
		}
			
		gameObj = getPrefab (id);
		cache.Add (id, gameObj);
		return gameObj;
	}
 
	private static GameObject getCached(int id){

		if (cache.ContainsKey (id)) {
			return cache [id];
		}

		return null;
	}
	private static GameObject getPrefab(int id){
		XmlDocument doc = XmlHelper.getXml ();
		XmlNode node = doc.SelectSingleNode ("//road[@id = '"+id+"']");
		if (node == null) {
			Debug.LogError ("Id:"+id + " road not found!");
			return null;
		}
		string name = node.Attributes ["name"].Value;
		return (GameObject)Resources.Load ("Prefabs/"+name);
	}
}


