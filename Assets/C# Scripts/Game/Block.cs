using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
public class Block
{
	private GameObject blueprintPrefab;
	private GameObject blueprintObject;

	private int id;
	private List<Vector2> collision;
	private int uuid;
	private Vector2? pos;
	private float rotation;
	private Texture2D placeholder;

	public Texture2D getPlaceholder(){
		return placeholder;
	}
	public Vector2 getWidthHeight(float deg){
		float width = 0;
		float height = 0;

		foreach(Vector2 collision in getCollision ()){
			Vector2 temp = VectorCalculation.rotateVector (collision, deg);
			width = Mathf.Max (width, Mathf.Abs(temp.x));
			height = Mathf.Max (height, Mathf.Abs(temp.y));
		}
		return new Vector2 ((width + 1) , ((height + 1) ));
	}
	public Block setPos(Vector2? pos){
		this.pos = pos;
		return this;
	}
	public Vector2? getPos(){
		return pos;
	}
	public Block setRotation(float rotation){
		this.rotation = rotation;
		return this;
	}
	public float getRotation(){
		return rotation;
	}
	public int getId(){
		return id;
	}
	public Block(int id, int uuid, List<Vector2> collision, GameObject blueprintPrefab, Texture2D placeholder){
		this.id = id;
		this.uuid = uuid;
		this.collision = collision;
		this.blueprintPrefab = blueprintPrefab;
		this.placeholder = placeholder;
	}
	public List<Vector2> getCollision(){
		return collision;
	}
	public GameObject getBlueprintPrefab(){
		if (blueprintObject == null || !blueprintObject.activeInHierarchy) {
			blueprintObject = Object.Instantiate (blueprintPrefab);
		}

		return blueprintObject;
	}
	public void removeBlueprintPrefab(){
		if (blueprintObject != null && blueprintObject.activeInHierarchy) {
			Object.Destroy (blueprintObject);
			blueprintObject = null;
		}
	}

	public int getUuid(){
		return uuid;
	}

	public string serialize(){
		if (getPos () == null) {
			return "";
		}
		Vector2 temp = (Vector2)getPos ();
		return temp.x + "," + temp.y + "," + getRotation();
	}

	public void unserialize(string text){
		string[] data = text.Split (',');
		Vector2 pos = new Vector2 ();
		pos.x = int.Parse (data [0]);
		pos.y = int.Parse (data [1]);
		setRotation (float.Parse (data [2]));
		setPos (pos);
		
	}

	public static Block createFromConfig(PieceConfig config){
		return new Block (config.id, 0, config.collision, config.prefab, config.placeholder);
	}
}
}

