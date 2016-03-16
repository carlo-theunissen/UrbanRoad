using System;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class AudioProvider
{
	private static Dictionary<string,AudioSource> cache;
	private AudioProvider (){}
	private static AudioProvider instance;
	private static void makeCache(){
		if(cache == null){
			cache = new Dictionary<string,AudioSource>();
		}
	}

	public static AudioSource getAudio(string name){
		makeCache ();

		AudioSource gameObj = getCached (name);
		if (gameObj != null) {
			return gameObj;
		}

		gameObj = getResource (name);
		cache.Add (name, gameObj);
		return gameObj;
	}

	private static AudioSource getCached(string id){

		if (cache.ContainsKey (id)) {
			return cache [id];
		}

		return null;
	}
		
	public static AudioProvider getInstance(){
		if (instance == null) {
			instance = new AudioProvider ();
		}
		return instance;
	}
	private static AudioSource getResource(string name){
		XmlDocument doc = XmlHelper.getXml ();
		XmlNode node = doc.SelectSingleNode ("//audio[@id = '"+name+"']");
		if (node == null) {
			Debug.LogError ("Audio:"+name + " not found!");
			return null;
		}
		string file = node.SelectSingleNode ("prefab").Attributes ["file"].Value;

		return (AudioSource) Resources.Load ("Audio/"+file);
	}
}


