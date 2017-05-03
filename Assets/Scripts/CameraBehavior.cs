using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public Transform player; //Informa que pro script funcionar ele precisa de um alvo, no caso o personagem.

	// Update is called once per frame
	void Update () {

		var posicaoNovaParaACamera = this.player.position; //Carrego a posicao do player para uma variavel
		posicaoNovaParaACamera.z = this.transform.position.z; //Mudo a posicao "z" da variavel pra a da Camera, mantendo a posicao "z" da Camera.

		this.transform.position = posicaoNovaParaACamera; //Mudo a posicao atual da Camera para a posicao nova.
	}
}
