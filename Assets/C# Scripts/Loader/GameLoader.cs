using System;

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	IEnumerator Start() {
		AsyncOperation async = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
		while (!async.isDone) {
			yield return null;
		}
		yield return new WaitForSeconds (.2f);
	}
}

