using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {
	[Header("Points and Teleports")]
	public GameObject[] points;
	public GameObject[] tps;

	[Header("Effects")]
	public FadeManager fadingEffect;

	[Header("Audio")]
	public AudioSource audioFile;
	public AudioClip[] clips;
	public int currentClip;
	public AudioSource porta;

	[Header("Endings Sprites and Bools")]
	public Image ending;
	public Sprite trueEnding;
	public Sprite normalEnding;
	public Sprite badEnding;
	public Sprite gameOver;
	public GameObject theEnd;
	public GameObject greatExclamation;
	private bool trueEndingCheck;
	private bool normalEndingCheck;
	private bool badEndingCheck;
	private bool gameOverCheck;

	[Header("References and Auxiliars")]
	private PlayerInteract playerInt;
	private int scoreAux;

	private void Awake () {
		playerInt = GetComponent<PlayerInteract> ();
		Fading (2.5f);
	}

	public void FixedUpdate(){
		if (trueEndingCheck) {
			Ending (trueEnding);
		} else if (normalEndingCheck) {
			Ending (normalEnding);
		} else if (badEndingCheck) {
			Ending (badEnding);
		} else if (gameOverCheck) {
			Ending (gameOver);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.position == tps [0].transform.position && (playerInt.score == 1 || playerInt.score == -1)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [1].transform.position && scoreAux == 0 && (playerInt.score == 1 || playerInt.score == -1)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [1].transform.position && scoreAux == 1 && (playerInt.score == 0 || playerInt.score == 2)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [2].transform.position && (playerInt.score == 2 || playerInt.score == 1)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [3].transform.position && (playerInt.score == 0)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [4].transform.position && scoreAux == 0 && (playerInt.score == -1 || playerInt.score == 1)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [4].transform.position && scoreAux == -1 && (playerInt.score == 0 || playerInt.score == -2)) {
			playerInt.canInteractWith = true;
		} else if (col.transform.position == tps [5].transform.position && (playerInt.score == -2 || playerInt.score == -1 || playerInt.score == 0)) {
			playerInt.canInteractWith = true;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (!Input.GetKeyDown(KeyCode.Space)) return;

		if (col.transform.position == tps [0].transform.position) {
			if (playerInt.score == 1) {
				UpdatePosition (0);
				scoreAux = playerInt.score;
				if (currentClip == 1)
					PlaySound (0);
			} else if (playerInt.score == -1) {
				UpdatePosition (2);
				if (currentClip == 0)
					PlaySound (1);
			}

		}

		if (col.transform.position == tps [1].transform.position) {
			if (scoreAux == 0) {
				if (playerInt.score == -1) {
					porta.Play ();
					UpdatePosition (3);
					scoreAux = playerInt.score;
					if (currentClip == 0)
						PlaySound (1);
				} else if (playerInt.score == 1) {
					porta.Play ();
					UpdatePosition (1);
					if (currentClip == 1)
						PlaySound (0);
				}
			} else if (scoreAux == 1) {
				if (playerInt.score == 0) {
					porta.Play ();
					UpdatePosition (3);
					scoreAux = playerInt.score;
					if (currentClip == 0)
						PlaySound (1);
				} else if (playerInt.score == 2) {
					porta.Play ();
					UpdatePosition (1);
					if (currentClip == 1)
						PlaySound (0);
				}
			}
		}

		if (col.transform.position == tps [2].transform.position) {
			if (playerInt.score == 2) {
				Debug.Log ("final bom");
				porta.Play ();
				trueEndingCheck = true;
				playerInt.canChangeVignette = false;
				UpdatePosition (5);
				if (currentClip == 1)
					PlaySound (0);
			} else if (playerInt.score == 1) {
				Debug.Log ("final regular");
				porta.Play ();
				normalEndingCheck = true;
				playerInt.canChangeVignette = false;
				UpdatePosition (6);
				if (currentClip == 1)
					PlaySound (0);
			}
		}

		if (col.transform.position == tps [3].transform.position) {
			if (playerInt.score == 0) {
				UpdatePosition (0);
				scoreAux = playerInt.score;
				if (currentClip == 1)
					PlaySound (0);
			}
		}

		if (col.transform.position == tps [4].transform.position) {
			if (scoreAux == 0) {
				if (playerInt.score == -1) {
					porta.Play ();
					UpdatePosition (4);
					if (currentClip == 0)
						PlaySound (1);
				} else if (playerInt.score == 1) {
					porta.Play ();
					UpdatePosition (1);
					if (currentClip == 1)
						PlaySound (0);
				}
			} else if (scoreAux == -1) {
				if (playerInt.score == 0) {
					porta.Play ();
					UpdatePosition (4);
					if (currentClip == 0)
						PlaySound (1);
				} else if (playerInt.score == -2) {
					porta.Play ();
					UpdatePosition (4);
					PlaySound (1);
				}
			}
		}

		if (col.transform.position == tps [5].transform.position) {
			if (playerInt.score == -2) {
				Debug.Log ("final morte -2");
				porta.Play ();
				gameOverCheck = true;
				playerInt.canChangeVignette = false;
				UpdatePosition (8);
				if (currentClip == 0)
					PlaySound (1);
			} else if (playerInt.score == -1) {
				Debug.Log ("final morte -1");
				porta.Play ();
				gameOverCheck = true;
				playerInt.canChangeVignette = false;
				UpdatePosition (8);
				if (currentClip == 0)
					PlaySound (1);
			} else if (playerInt.score == 0) {
				Debug.Log ("final ruim");
				porta.Play ();
				badEndingCheck = true;
				playerInt.canChangeVignette = false;
				UpdatePosition (7);
				if (currentClip == 0)
					PlaySound (1);
			}
		}
	}

	void Fading(float fadeSpeed){
		fadingEffect.Fade (true, fadeSpeed);
		fadingEffect.Fade (false, fadeSpeed);

		playerInt.canScore = true;
	}

	void UpdatePosition (uint point) {
		Fading (1.5f);
		transform.position = points [point].transform.position;
	}

	void PlaySound(int clip) {
		audioFile.clip = clips [clip];
		audioFile.Play ();
		currentClip = clip;
	}	

	void Ending(Sprite typeOfEnd){
		Fading (3.0f);
		if (typeOfEnd != gameOver){
			playerInt.vinheta.sprite = null;
		}

		if(typeOfEnd == gameOver){
			greatExclamation.SetActive (true);
		}

		ending.sprite = typeOfEnd;
		gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		theEnd.SetActive (true);

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Fading (3.0f);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
		}
	}
}
