using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Transform player;

	void Start () {
		GameObject.Instantiate (player, player.transform.position, Quaternion.identity);
	}
	

}
