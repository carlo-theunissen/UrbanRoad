using System;

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	IEnumerator Start() {
		yield return new WaitForSeconds (.1f);
		AsyncOperation async = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
		while (!async.isDone) {
			yield return null;
		}
	}
}

