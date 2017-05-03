using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComLimiteBehavior : MonoBehaviour {

	public Transform player; //Informa que pro script funcionar ele precisa de um alvo, no caso o personagem.

	public float alturaMinima = 1.56f;
	public float alturaMaxima = 4.9f;

	// Update is called once per frame
	void Update () {

		var posicaoNovaParaACamera = this.player.position; //Carrego a posicao do player para uma variavel
		posicaoNovaParaACamera.z = this.transform.position.z; //Mudo a posicao "z" da variavel pra a da Camera, mantendo a posicao "z" da Camera.

		//Validar Alturas --------
		if (posicaoNovaParaACamera.y <= alturaMinima) {
			posicaoNovaParaACamera.y = alturaMinima;
		}

		if (posicaoNovaParaACamera.y >= alturaMaxima) {
			posicaoNovaParaACamera.y = alturaMaxima;
		}
		//------------------------

		this.transform.position = posicaoNovaParaACamera; //Mudo a posicao atual da Camera para a posicao nova.
	}
}
