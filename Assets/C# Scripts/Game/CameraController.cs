using UnityEngine;
using System.Collections;
using System.Xml;
namespace Game{
	public class CameraController : MonoBehaviour {

		// Use this for initialization
		void Start () {
			XmlDocument doc = XmlHelper.getXml ();
			Level level = GameMode.getCurrentLevel ();
			XmlNode node = doc.SelectSingleNode ("//camera[@width = '"+level.getWidth()+"' and @height = '"+level.getHeight()+"']");
			if (node == null) {
				Debug.LogError ("Please add the camera config in the xml file");
				return;
			}
			XmlNode position = node.SelectSingleNode ("position");


			transform.position = new Vector3 (float.Parse (position.Attributes ["x"].Value), float.Parse (position.Attributes ["y"].Value), float.Parse (position.Attributes ["z"].Value));
			GetComponent<Camera> ().orthographicSize = float.Parse (position.Attributes ["size"].Value);
		}

	}
}