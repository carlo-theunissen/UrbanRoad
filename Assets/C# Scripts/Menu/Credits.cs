using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float speed = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed, Space.World);

        if (transform.position.y > 900)
        {
            SceneManager.LoadScene("Menu_start");

        }
    }
}
