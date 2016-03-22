using System;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class RoadPieceHelper
{
	private static Dictionary<int,GameObject> cache;
	private static Dictionary<int,int> rotationCache;

	private static void makeCache(){
		if(cache == null){
			cache = new Dictionary<int,GameObject>();
		}
	}

	private static void makeRotationCache(){
		if(rotationCache == null){
			rotationCache = new Dictionary<int,int>();
		}
	}

	public static int getRotation(int id){
		
		makeRotationCache ();
		int? rotation = getRotationCached (id);

		if (rotation != null) {
			return (int)rotation;
		}


		rotation = calculateRotation (id);
		rotationCache.Add (id, (int) rotation);
		return (int) rotation;
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

	private static int? getRotationCached(int id){
		if (rotationCache.ContainsKey (id)) {
			return rotationCache [id];
		}

		return null;
	}
	private static XmlNode getNode(int id){
		XmlDocument doc = XmlHelper.getXml ();
		XmlNode node = doc.SelectSingleNode ("//road[@id = '"+id+"']");
		if (node == null) {
			Debug.LogError ("Id:"+id + " road not found!");
			return null;
		}
		return node;
	}
	private static GameObject getPrefab(int id){
		XmlNode node = getNode (id);
		string name = node.SelectSingleNode ("prefab").Attributes ["name"].Value;
		return (GameObject) Resources.Load ("Prefabs/"+name);
	}
	private static int? calculateRotation(int id){
		XmlNode node = getNode (id);
		string value = node.SelectSingleNode ("rotation").Attributes ["value"].Value;
		return int.Parse (value);
	}
}


