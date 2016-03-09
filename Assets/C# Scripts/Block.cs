using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Block
{
	private GameObject blueprintPrefab;
	private GameObject blueprintObject;

	private int id;
	private List<Vector2> collision;
	private int uuid;
	private Vector2 pos = new Vector2(-1,-1);
	private float rotation;
	private Texture2D placeholder;

	public Texture2D getPlaceholder(){
		return placeholder;
	}

	public Block setPos(Vector2 pos){
		this.pos = pos;
		return this;
	}
	public Block clearPos(){
		pos = new Vector2(-1,-1);
		return this;
	}
	public Block setRotation(float rotation){
		this.rotation = rotation;
		return this;
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

	public int getUuid(){
		return uuid;
	}

	public string serialize(){
		return "";
	}

	public void unserialize(string text){
	}

	public static Block createFromConfig(PieceConfig config){
		return new Block (config.id, 0, config.collision, config.prefab, config.placeholder);
	}
}


