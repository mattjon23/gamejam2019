using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour {

	public PlayerInteract playerRef;

	void Interacted()
	{
		playerRef.imagemDaMoldura.sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
	}

	void PositiveFirst(){
		if (playerRef.canScore) {
			playerRef.score += 1;
			playerRef.canScore = false;
		}
	}

	void NegativeFirst()
	{
		if (playerRef.canScore) {
			playerRef.score -= 1;
			playerRef.canScore = false;
		}
	}
}
