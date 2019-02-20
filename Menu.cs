using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour{

	public Image bg;
	public Sprite about;
	public Sprite menu;

	public void Play(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void ChangeBGabout()
	{
		bg.sprite = about;
	}

	public void ChangeBGmenu()
	{
		bg.sprite = menu;
	}

	public void Quit(){
		Application.Quit ();
	}
}
