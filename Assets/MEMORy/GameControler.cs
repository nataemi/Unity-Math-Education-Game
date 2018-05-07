using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameControler : MonoBehaviour {

	[SerializeField]
	private Sprite bgImage;

	public List<Sprite> puzzles;

	public List<Sprite> gamePuzzles = new List<Sprite> ();

	public List<Button> btns = new List<Button>();

	private bool firstGuess, secoundGuess;

	private int countGuesses;
	private int countCorrectGuesses;
	private int gameGuesses;

	private int firstGuessIndex, secoundGuessIndex;

	private string firstGuessPuzzle, secoundGuessPuzzle;

	// od tego momentu cos mieszam jbc to to wypierdol
	[SerializeField]
	private GameObject puzzleField;

	[SerializeField]
	private GameObject endPanel;




	void Awake(){
		Sprite[] puzzlesTemp = Resources.LoadAll<Sprite> ("PEK 10");

		for (int i = 0; i < puzzlesTemp.Length; i++) {
			puzzles.Add (puzzlesTemp [i]);
		}

	}

	IEnumerator Start(){
		GetButtons();
		AddListeners ();
		AddGamePuzzles ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;
		yield return new WaitForSeconds (2.0f);
	}

	void GetButtons(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Puzzle Button");

		for (int i = 0; i < objects.Length; i++) {
			btns[i] = objects[i].GetComponent<Button>();
			btns [i].image.sprite = bgImage;
		}
	}

	void AddGamePuzzles(){
		int looper = btns.Count;


		for (int i = 0; i < looper/2; i++) {
			// zrob tak zeby random sie nie powtarzalo jakos !!!
			// uwazaj bo moze byc tak ze jeden wynik do kilku]
			int index = 2* Random.Range (0, puzzles.Count/2);
			while (gamePuzzles.Contains (puzzles [index]) == true) {
				index = 2* Random.Range (0, puzzles.Count/2);
			}
			gamePuzzles.Add (puzzles [index]);
			Sprite add = puzzles.Find(item => item.name == puzzles[index].name+"odp"); 
			gamePuzzles.Add (add);
		}
	}

	void AddListeners(){
		foreach (Button btn in btns) {
			btn.onClick.AddListener (() => PickPuzzle ());
		}
	}

	public void PickPuzzle(){

		if (!firstGuess) {
			firstGuess = true;
			firstGuessIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			firstGuessPuzzle = gamePuzzles [firstGuessIndex].name;
			btns [firstGuessIndex].image.sprite = gamePuzzles [firstGuessIndex];
		} else if (!secoundGuess) {
			secoundGuess = true;
			secoundGuessIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			secoundGuessPuzzle = gamePuzzles [secoundGuessIndex].name;
			btns [secoundGuessIndex].image.sprite = gamePuzzles [secoundGuessIndex];
			countGuesses++;
			StartCoroutine (CheckIfThePuzzlesMatch());
		}


	}

	IEnumerator CheckIfThePuzzlesMatch(){
		yield return new WaitForSeconds (.1f);
		if (firstGuessPuzzle == secoundGuessPuzzle+"odp" || secoundGuessPuzzle == firstGuessPuzzle+"odp") {
			yield return new WaitForSeconds (.1f);
			btns [firstGuessIndex].interactable = false;
			btns [secoundGuessIndex].interactable = false;

			btns [firstGuessIndex].image.color = new Color (0, 0, 0, 0);
			btns [secoundGuessIndex].image.color = new Color (0, 0, 0, 0);
			CheckIfTheGameIsFinished ();
		} else {
			yield return new WaitForSeconds (.1f);
			btns [firstGuessIndex].image.sprite = bgImage;
			btns [secoundGuessIndex].image.sprite = bgImage;

		}

		yield return new WaitForSeconds (.1f);

		firstGuess = secoundGuess = false;
	}

	void CheckIfTheGameIsFinished(){
		countCorrectGuesses++;

		if (countCorrectGuesses == gameGuesses) {
			Debug.Log ("Game finished");
			Debug.Log ("It took you:" + countGuesses);


			puzzleField.SetActive (false);
			endPanel.SetActive (true);
			GameObject textfield = GameObject.FindGameObjectWithTag("Congrats");
			textfield.GetComponent<Text> ().text = "Gratulacje! Potrzebowałeś " + countGuesses + " prób.";
			GameObject button =  GameObject.FindGameObjectWithTag("Restart");
			Button myButton =  button.GetComponent<Button>();
			myButton.onClick.AddListener (() => {runScene.NextScene("MEMORY");}); 
			GameObject button2 =  GameObject.FindGameObjectWithTag("Powrot");
			Button myButton2 =  button2.GetComponent<Button>();
			/*GameObject klasa1 =  GameObject.FindGameObjectWithTag("Klasa 1");
			klasa1.SetActive (true);
			GameObject gry =  GameObject.FindGameObjectWithTag("Gry");
			gry.SetActive (false); to nie dziala a szkoda*/ 
			myButton2.onClick.AddListener (() => {runScene.NextScene("MENU");});
			// tu trzeba to jakos zrobic zeby odpalalo tego peka co chcesz , ewentualnie zrobic tak zeby wracalo do klasy 1

		}
	}

	void Shuffle(List<Sprite> list){

		for (int i = 0; i < list.Count; i++) {
			Sprite temp = list[i];
			int randomindex = Random.Range (0, list.Count);
			list [i] = list [randomindex];
			list [randomindex] = temp;
		}


	}
		

}
// zeby przerobic tak zeby byly rzymskie i arabskie to pewnie musisz zrobic np name + "a" i dodac arabskie z nazwami 1a , 2a CHYBA 
// i wtedy przy pobieraniu gry tez jakos tak ustaw zeby gralo
