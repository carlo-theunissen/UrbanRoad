using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public GameObject plane;


    private GameObject[,] grid;
	private Level level;

	private void makeGrid(){

		for (int x = 0; x < level.getWidth (); x++) 
		{
			for (int z = 0; z < level.getHeight (); z++)
			{
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(x,
					0, z);
				grid[x, z] = gridPlane;
			}
		}
	}


	public void placeDummy(float x, float z, GameObject place, float deg = 0 ){


		place.transform.position = new Vector3 (x, 0, z);
		if (place.transform.rotation.eulerAngles.y != deg) {
			place.transform.Rotate (new Vector3 (0, deg, 0));
		}

	}

	// Use this for initialization
	void Start ()
    {
		level = GameMode.getCurrentLevel ();
		grid = new GameObject[level.getWidth (), level.getHeight ()];
		makeGrid ();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
