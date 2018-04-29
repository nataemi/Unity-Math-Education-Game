using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PEKManager : MonoBehaviour {

	static int PEK;

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private GameObject btn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setPEK(int p){
		PEKManager.PEK = p;
		Debug.Log (PEKManager.PEK);
	}

	public void setPEKName(){
		// tu trzeba dodac zeby sobie pobralo z bazy nazwe PEKA i wpisalo ja w tekst zamiast numer PEKA
		GameObject textfield = GameObject.FindGameObjectWithTag("PEKName");
		textfield.GetComponent<Text> ().text = PEKManager.PEK.ToString();
	}

	//musi sobie pobrac z bazy ile jest gier i jakie sa ich nazwy
	public void createGameButtons(){
		int amountOfGames = 2; // pobierz ta liczbe z bazy na podstawie PEKA
		string[] gameTitles = { "Memory", "Quiz"}; // pobierz to z bazy na podstawie PEKA
		string[] sceneTitles = {"MEMORY", "GamePlay"}; // pobierz z bazy nazwy scen odpalajacych gry
		panel.DetachChildren (); // co tu se dzieje z jego dziecmi? one sa usuwane ? czy WYCIEK PAMIECI!!!!
		for (int i = 0; i < amountOfGames; i++) {
			GameObject button = Instantiate (btn);
			button.tag = i.ToString();
			button.name = i.ToString();
			button.transform.SetParent (panel,false);
			Button myButton =  button.GetComponent<Button>();
			string sceneName = sceneTitles [i];
			myButton.onClick.AddListener (() => {runScene.NextScene(sceneName);}); // uwaga tu nazwy scen tez musze zostac pobrane po PEKU albo nazywac sie tak samo jak gra
			Text textfield = button.GetComponentsInChildren<Text>()[0];
			textfield.text = gameTitles [i];
		}
	}
}
