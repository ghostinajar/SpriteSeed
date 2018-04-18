using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpellController : MonoBehaviour {

	public delegate void SpellDelegate (Tilemap tileMap, TileBase tileBase, Vector3Int position);
	public SpellDelegate spell1;
	public SpellDelegate spell2;
	public Vector3 mousePos;
	public Vector3 mousePosWorld;
	public Vector3Int clickedCell;
	public Tilemap tileMap;
	public TileBase tileBase;
	public GameObject testObj;
	void Start () {
		spell1 = TillSoil;
	}

	void Update () {

	}

	public void TillSoil(Tilemap tileMap, TileBase tileBase, Vector3Int position) {
		tileMap.SetTile (position, tileBase);
		tileMap.RefreshTile (position);
		//GameObject.Instantiate (testObj, position, Quaternion.identity);
	}


	void OnGUI() {
		Camera  c = Camera.main;
		bool fire1 = Input.GetButton ("Fire1");
		if (fire1) {
			mousePos = Input.mousePosition;
			mousePosWorld = c.ScreenToWorldPoint (mousePos);
			clickedCell = tileMap.WorldToCell (mousePosWorld);
			spell1 (tileMap, tileBase, clickedCell);
		}
	}

}
