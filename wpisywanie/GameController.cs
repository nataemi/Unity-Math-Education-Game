using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private int num;

    private int CountGuesses;

    [SerializeField]
    private InputField input;

    [SerializeField]
    private GameObject btn;

    [SerializeField]
    private Text text;

    void Awake()
    {
        num = Random.Range(0, 101);
        text.text = "Zgadnij liczbę, o której myśli stworek. Liczba jest w przedziale od 0 do 100";
    }

    // void Awake() {
    //   liczba1 = Random.Range(0,101);
    //   liczba2 = Random.Range(0,101);

    public void getInput(string guess)
    {
        CompareGuesses(int.Parse(guess));
        Debug.Log("You entered " + guess);
        input.text = "";
        CountGuesses++;
    }

    public void CompareGuesses(int guess)
    {
        if (guess == num)
        {
            text.text = "Udało się! Zgadłeś poprawnie liczbę, o której myślał stworek. Ta liczba to " + guess + ". Udało Ci się to za " + CountGuesses + " razem." +
            " Chcesz zagrac jeszcze raz?";
            btn.SetActive(true);
        }
        else if (guess > num)
        {
            text.text = "Próbuj dalej. Liczba, o której myśli stworek jest mniejsza niż " + guess;
        }
        else if (guess < num)
        {
            text.text = "Próbuj dalej. Liczba, o której myśli stworek jest większa niż " + guess;
        }
    }
    public void PlayAgain()
    {
        num = Random.Range(0, 101);
        text.text = "Zgadnij liczbę, o której myśli stworek. Liczba jest w przedziale od 0 do 100";
        CountGuesses = 0;
        btn.SetActive(false);
    }
}



