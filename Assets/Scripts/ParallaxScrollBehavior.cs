using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrollBehavior : MonoBehaviour {

	public Vector2 speed = new Vector2 (0.002f, 0.001f); //Define a velocidade em x e y do movimento dos backgrounds
	public Vector2 ajuste = new Vector2 (40f, 10f); //Define um ajuste de velocidade, caso seja necessario diminuir ainda mais as mesmas.

	private Material material; //Carrega o material "imagem" do background para fazer o movimento.
	private GameObject player; //Carrega o player para identificar a direção que o mesmo segue.

	private Vector2 pos = Vector2.zero; //Valores de posicionamento atualizados em x e y, iniciando em (0, 0).

	// Use this for initialization
	void Start () {
		this.material = GetComponent<Renderer> ().material; //Carrega o componente Material do GameObject
		this.player = GameObject.FindGameObjectWithTag ("Player"); //Carrega o player da Scene
	}
	
	// Update is called once per frame
	void Update () {

		var ve1 = this.player.GetComponent<Rigidbody2D> ().velocity.x; //Identifica a velocidade aplicada ao Player em X.
		var ve2 = this.player.GetComponent<Rigidbody2D> ().velocity.y; //Identifica a velocidade aplicada ao Player em Y.

		if (ve1 != 0f) {
			var side = player.transform.localScale.x; //Identifica direção (Direita ou Esquerda).
			this.pos.x += speed.x / ajuste.x * side; //Identifica a nova posicao em X do background.

		}
			
		if (ve2 != 0f) {
			var dir = ve2 / Mathf.Abs (ve2); //Identifica direção (pra Cima ou pra Baixo).
			this.pos.y += speed.y / ajuste.y * dir; //Identifica a nova posicao em Y do background.
		}

		this.material.mainTextureOffset = this.pos; //Atualização do offset do background

	}

}
