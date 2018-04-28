using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{

    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 1f;
     public  static int totalCorrect = 0;
     public  static int totalGuesses = 0;



    void Start()
    {       

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        SetCurrentQuestion();

    }
    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "BRAWO!";
            falseAnswerText.text = "ŹLE :(";
        }
        else
        {
            trueAnswerText.text = "ŹLE :(";
            falseAnswerText.text = "BRAWO!";
        }

    }
    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
    public void UserSelectTrue()
    {
		Debug.Log(GameManager2.totalGuesses +"totalguesses0");

        animator.SetTrigger("True");
        //score
		GameManager2.totalGuesses = GameManager2.totalGuesses +  1;
		Debug.Log(GameManager2.totalGuesses+"totalguesses1");

        if (currentQuestion.isTrue)
        {
            Debug.Log("BRAWO!");
            //score 
			GameManager2.totalCorrect =GameManager2.totalCorrect+ 1;
            IfGameFinished();
        }
        else
        {
            Debug.Log("ŹLE :(");
            IfGameFinished();
        }
        StartCoroutine(TransitionToNextQuestion());
    }
    public void UserSelectFalse()
    {
        
        animator.SetTrigger("False");
        //score
		GameManager2.totalGuesses += 1;
        if (currentQuestion.isTrue)
        {
            Debug.Log("ŹLE :(");
            IfGameFinished();
        }
        else
        {
			Debug.Log(GameManager2.totalGuesses + "totalguesses0");
            Debug.Log("BRAWO!");
            //score
			GameManager2.totalCorrect = GameManager2.totalCorrect+ 1;
			Debug.Log(GameManager2.totalGuesses + "totalguesses1");
            IfGameFinished();
        }
        StartCoroutine(TransitionToNextQuestion());
    }
    public void IfGameFinished()
    {
		if (GameManager2.totalCorrect == 3)
        {
            Debug.Log("Game finished");
			Debug.Log("zdobyles " + GameManager2.totalCorrect);
        }
        else
        {
			Debug.Log("correct: "+GameManager2.totalCorrect+", all: " +GameManager2.totalGuesses);
        }

    }
    public void quit()
    {
        Debug.Log("has quit game");
        Application.Quit();
    }

}
