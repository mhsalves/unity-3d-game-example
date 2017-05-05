using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerBehavior))]
public class PlayerControleBehavior : MonoBehaviour {

	private PlayerBehavior playerBehavior; //Para trabalhar com o comportamento do Player, previamente codificado.
	private bool m_Pulando; //FLAG de pulo para controle neste classe também.

	private void Awake() {
		this.playerBehavior = GetComponent<PlayerBehavior> (); //Carregar o Componente PlayerBehavior.
	}
		
	
	// Update is called once per frame
	void Update () {

		if (!m_Pulando) { //Identificando o botão de PULAR, SOMENTE se já não estiver pulando.
			this.m_Pulando = Input.GetButtonDown ("Jump"); //Com isso, atualiza a variável de controle local.
		}

	}

	void FixedUpdate() {

		bool correr = Input.GetKey (KeyCode.LeftShift); //Identifica se o botão de correr foi acionado
		float movimento = Input.GetAxis ("Horizontal"); //Identifica se o botão de andar horizontalmente foi acionado

		playerBehavior.Mover (movimento, correr, m_Pulando); //Ativa a função de movimento, mesmo que o movimento seja 0.
		m_Pulando = false;//E volta a variável de controle de PULO para falso.

	}
}
