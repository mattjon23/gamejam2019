using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {

	[Header("Interactables")]
	public GameObject currentInteractable = null;
	public GameObject atencaoRend;

	public GameObject molduraImagem;
	public Image imagemDaMoldura;

	[Header("Checkers")]
	public bool canInteract;
	public bool seeingImage;
	public bool canInteractWith = false;
	public bool canScore = true;
	public bool canChangeVignette = true;

	public int score = 0;

	[Header("Audio")]
	public AudioSource audioSrc;

	[Header("Images and Sprites")]
	public Image vinheta;
	public Sprite vinhetaRuim;
	public Sprite vinhetaBoa;
	public Sprite vinhetaNeutra;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("PositiveScore") || other.CompareTag ("NegativeScore")) {
			currentInteractable = other.gameObject;
			Atencao();
			canInteract = true;
		} else if (other.CompareTag ("InteractableObject") && canInteractWith) {
			currentInteractable = other.gameObject;
			Atencao ();
			canInteractWith = false;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag ("PositiveScore") || other.CompareTag ("NegativeScore")) {
			canInteract = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("PositiveScore") || other.CompareTag ("NegativeScore")) {
			if (other.gameObject == currentInteractable) {
				SemAtencao();
				currentInteractable = null;
				canInteract = false;
			}
		} else if (other.CompareTag ("InteractableObject")) {
			if (other.gameObject == currentInteractable) {
				SemAtencao ();
				currentInteractable = null;
			}
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) && canInteract && !seeingImage) {
			currentInteractable.SendMessage ("Interacted");

			if(canScore){
				audioSrc.Play ();
			}
			canInteract = false;
			seeingImage = true;

			molduraImagem.SetActive (true);

			gameObject.GetComponent<PlayerMovement>().enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

			if (currentInteractable.CompareTag ("PositiveScore")) {
				currentInteractable.SendMessage ("PositiveFirst");
			}
			if (currentInteractable.CompareTag ("NegativeScore")) {
				currentInteractable.SendMessage ("NegativeFirst");
			}

		} else if (Input.GetKeyDown (KeyCode.Space) && seeingImage) {
			seeingImage = false;
			canInteract = true;
			molduraImagem.SetActive (false);
			gameObject.GetComponent<PlayerMovement>().enabled = true;
		}

		if (score == 0 && canChangeVignette)
			vinheta.sprite = vinhetaNeutra;
		else if (score > 0 && canChangeVignette)
			vinheta.sprite = vinhetaBoa;
		else if (score < 0 && canChangeVignette)
			vinheta.sprite = vinhetaRuim;
	}

	void Atencao(){
		atencaoRend.SetActive (true);
	}

	void SemAtencao(){
		atencaoRend.SetActive (false);
	}
		
}
