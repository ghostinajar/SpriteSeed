using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : MonoBehaviour {

	//prepare rigidbody2D and animator component references
	private Rigidbody2D rb;
	private Animator anim;

	//player or npc move and spell parameters
	public float moveSpeed;
	public float spellRate;
	private bool spellReady;
	public bool spellEquipped;
	private float moveX;
	private float moveY;
	public MoverType moverType;

	public enum MoverType {Player, Npc}

	//NPC-only AI parameters
	public float changeDirectionRate;
	private bool changeDirectionReady;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		spellReady = true;
		changeDirectionReady = true;
	}

	void FixedUpdate () {
		if (moverType == MoverType.Player) {
			MoveRigidBodyPlayer ();
			CheckSpellPlayer ();
		}
		if (moverType == MoverType.Npc) {
			MoveRigidBodyNPC ();
			CheckSpellNPC ();
		}
		SendMovementToAnimator ();
	}

	void MoveRigidBodyPlayer () {
		moveX = Input.GetAxisRaw ("Horizontal");
		moveY = Input.GetAxisRaw ("Vertical");
		Vector2 move = new Vector2(moveX,moveY);
		rb.velocity = move * moveSpeed;
	}

	void MoveRigidBodyNPC () {
		if (changeDirectionReady == true) {
			int newDirection = Random.Range (1, 5);
			switch (newDirection)
			{
			case 1:
				moveX = 1;
				moveY = 0;
				break;
			case 2:
				moveX = -1;
				moveY = 0;
				break;
			case 3:
				moveX = 0;
				moveY = 1;
				break;
			case 4:
				moveX = 0;
				moveY = -1;
				break;
			}
			changeDirectionReady = false;
			StartCoroutine (WaitForChangeDirectionReady ());
		}
		Vector2 move = new Vector2(moveX,moveY);
		rb.velocity = move * moveSpeed;
	}
		
	void CheckSpellPlayer () {
		//get player spell input
		bool spellInput = Input.GetButton ("Fire1");
		//check conditions, send to Animator, restart timer coroutine
		if (spellInput && spellEquipped && spellReady == true) {
			anim.SetTrigger ("Spell");
			spellReady = false;
			StartCoroutine (WaitForSpellReady ());
		}
	}

	void CheckSpellNPC () {
		//check conditions, send to Animator, restart timer coroutine
		if (spellEquipped && spellReady == true) {
			anim.SetTrigger ("Spell");
			spellReady = false;
			StartCoroutine (WaitForSpellReady ());
		}
	}

	//spell timer coroutine
	IEnumerator WaitForSpellReady () {
		yield return new WaitForSeconds(spellRate);
		spellReady = true;
	}

	IEnumerator WaitForChangeDirectionReady () {
		yield return new WaitForSeconds (changeDirectionRate);
		changeDirectionReady = true;
	}

	void SendMovementToAnimator () {
		anim.SetBool ("IsMoving", false);
		if (moveX != 0 || moveY != 0) {
			anim.SetBool ("IsMoving", true);
			anim.SetFloat ("LastX", moveX);
			anim.SetFloat ("LastY", moveY);
			anim.SetFloat ("MovingX", moveX);
			anim.SetFloat ("MovingY", moveY);
		}
	}

}