using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerBehavior))]
public class VerificadorDeMorte : MonoBehaviour {

	private PlayerBehavior m_PlayerBehavior;

	void Awake () {
		this.m_PlayerBehavior = GetComponent<PlayerBehavior> ();

	}

	void OnCollisionEnter2D(Collision2D coll) {

		print (coll.gameObject.layer);

		if (coll.gameObject.tag == "Lava") {

			m_PlayerBehavior.Morrer ();

		}

	}


}
