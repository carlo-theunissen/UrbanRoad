using System;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class AudioProvider
{
	private Dictionary<string,AudioClip> cache;
	private AudioProvider (){}
	private static AudioProvider instance;
	private AudioSource cachedSource; 

	private void makeCache(){
		if(cache == null){
			cache = new Dictionary<string,AudioClip>();
		}
	}
	public void playAudio(string name){
		if (cachedSource == null) {
			GameObject go = GameObject.Find ("SoundPlayer");
			cachedSource = go.GetComponent<AudioSource> ();
		}
		playAudio (name, cachedSource, false);
	}

	public void playAudio(string name, AudioSource source, bool loop =false){
		source.clip = getAudio (name);
		source.loop = loop;
		source.Play ();
	}

	private AudioClip getAudio(string name){
		makeCache ();

		AudioClip gameObj = getCached (name);
		if (gameObj != null) {
			return gameObj;
		}

		gameObj = getResource (name);
		cache.Add (name, gameObj);
		return gameObj;
	}

	private AudioClip getCached(string id){

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
	private AudioClip getResource(string name){
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


