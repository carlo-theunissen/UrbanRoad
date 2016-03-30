using System;

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	IEnumerator Start() {
		AsyncOperation async = SceneManager.LoadSceneAsync("Game");
		yield return async;
	}
}

