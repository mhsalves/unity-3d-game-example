using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	[SerializeField] private LayerMask m_OQueEChao; //Listar Layer que indicam quais objetos são chão 
	[SerializeField] bool m_MoverNoAr = true; //Indicar se pode haver movimento para os lados durante o pulo.
	[SerializeField] float m_ForcaDoPulo = 400f; //Valor da força aplicada para pulo.
	[SerializeField] float m_ForcaDoPuloEmCorrida = 500f; //Valor da força aplicada para pulo.
	[SerializeField] float m_VelocidadeNormal = 6f; // Valor de velocidade normal (Andando)
	[SerializeField] float m_VelocidadeRapida = 12f; //Valor de velocidade Rapida (Correndo)

	private bool m_PodeAndar = true;
	private bool m_Morto = false;

	private Transform m_Pe_ChaoCheck; // Objeto que marca o pé do player.
	private bool m_NoChao; //FLAG que indica se o Player está tocando Chão.

	private Animator m_Animator; //Componente de Animação
	private Rigidbody2D m_Rigidbody2D; //Componente de Rigidbody

	private const float k_RadioDeChao = .2f; //Valor da distancia a ser detectada proximo do "Chão"

	private bool m_EstaViradoPraDireita = true;//FLAG indicando direção do Player (Direita ou Esquerda)

	private void Awake() {

		this.m_Pe_ChaoCheck = transform.Find ("Pe_ChaoCheck"); //Carregando "Objeto Filho" com nome "Pe_ChaoCheck"

		this.m_Animator = GetComponent<Animator> (); //Carregando o componente de Animação (Animator)
		this.m_Rigidbody2D = GetComponent<Rigidbody2D> (); //Carregando o componente de Corpo (Rigidbody2D)

	}

	private void FixedUpdate() {
		
		this.m_NoChao = false;

		/*
		 * Verificando os objetos que estão entrando em colisão com o objeto marcado como Pé (m_Pe_ChaoCheck)
		 * A uma certa distância "k_RadioDeChao"
		 * Indicados pelos seguintes Layers (m_OQueEChao)
		*/
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_Pe_ChaoCheck.position, k_RadioDeChao, m_OQueEChao);
		// E se qualquer um desses objetos for diferente do Player (que pode acontecer), significa que o PLAYER está no chão
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				this.m_NoChao = true;
		}

		//Atualiza as variáveis de animação no Animator.
		this.m_Animator.SetBool("NoChao", m_NoChao);
		this.m_Animator.SetFloat("vVelocidade", m_Rigidbody2D.velocity.y);

	}

	//SERVE PARA INVERTER O LADO DO SPRITE EM X.
	private void VirarEmX() {
		
		this.m_EstaViradoPraDireita = !this.m_EstaViradoPraDireita;

		Vector3 escalaAtual = this.transform.localScale;
		escalaAtual.x *= -1;
		transform.localScale = escalaAtual;

	}

	public void Mover( float movimento, bool correndo, bool pulando ) {

		//Verifica se o Player está no chao ou se pode mover no ar para poder efetuar movimento.
		if ( (m_NoChao || m_MoverNoAr) && m_PodeAndar ) {

			//Atualiza variável no Animator
			m_Animator.SetFloat ("Velocidade", Mathf.Abs(movimento));

			//Identifica qual das velocidades será usada, a de Correr ou Caminhar.
			var vel = (correndo) ? m_VelocidadeRapida : m_VelocidadeNormal;

			//Aplica a força velocidade nova em X e mantém a velocidade de Y, pois pode ser que o mesmo esteja no ar.
			this.m_Rigidbody2D.velocity = new Vector2(movimento * vel, m_Rigidbody2D.velocity.y);

			//Valida a direção que segue e vira o Sprite se for necessario ----
			if (movimento > 0 && !m_EstaViradoPraDireita) {
				this.VirarEmX ();
			}

			if (movimento < 0 && m_EstaViradoPraDireita) {
				this.VirarEmX ();
			}
			//------------------------------------------------------------------

			/*
			 * Caso o movimento peça para pular, deve ser verificado se o mesmo está no chão
			 * E caso esteja tudo certo, atualiza...
			*/
			if (m_NoChao && pulando && m_Animator.GetBool ("NoChao")) {
				m_NoChao = false; //Atualiza a FLAG de estar no Chão
				m_Animator.SetBool ("NoChao", false); //Atualiza a variável do Animator

				var forca = (correndo) ? m_ForcaDoPuloEmCorrida : m_ForcaDoPulo;

				m_Rigidbody2D.AddForce(new Vector2(0f, forca)); //Aplica força no eixo Y, simulando o PULO.
			}

		}

	}

	public void Morrer () {

		if (!this.m_Morto) {
			this.m_PodeAndar = false;
			this.m_Morto = true;

			this.m_Animator.SetBool ("Morto", m_Morto);
			this.m_Animator.SetTrigger ("Morrer");
		}
	}

}
