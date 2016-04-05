using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float speed = 2;
	public float max = 900;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed, Space.World);

        if (transform.position.y > max)
        {
            SceneManager.LoadScene("Menu_start");

        }
    }
}
