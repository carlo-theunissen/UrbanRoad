using System;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class AudioProvider
{
	private static Dictionary<string,AudioClip> cache;
	private AudioProvider (){}
	private static AudioProvider instance;
	private static void makeCache(){
		if(cache == null){
			cache = new Dictionary<string,AudioClip>();
		}
	}

	public AudioClip getAudio(string name){
		makeCache ();

		AudioSource gameObj = getCached (name);
		if (gameObj != null) {
			return gameObj;
		}

		gameObj = getResource (name);
		cache.Add (name, gameObj);
		return gameObj;
	}

	private static AudioClip getCached(string id){

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
	private static AudioClip getResource(string name){
		XmlDocument doc = XmlHelper.getXml ();
		XmlNode node = doc.SelectSingleNode ("//audio[@name = '"+name+"']");
		if (node == null) {
			Debug.LogError ("Audio:"+name + " not found!");
			return null;
		}
		string file = node.Attributes ["file"].Value;

		return (AudioClip) Resources.Load ("Audio/"+file);
	}
}


