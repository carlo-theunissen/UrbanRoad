using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    public GameObject follow;
    public bool active = false;
  

	// Use this for initialization
	void Start () {


	}
    public void setFollow(GameObject follow)
    {
        this.follow = follow;
    }
    public void setActive(bool active)
    {
        this.active = active;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Input.touchCount);
        if (Input.touchCount > 0 && follow != null && active)
        {
            follow.transform.position = Input.GetTouch(0).position;
        }
	
	}
}
