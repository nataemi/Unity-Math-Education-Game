
using UnityEngine;
using UnityEngine.SceneManagement;

public class runScene : MonoBehaviour
{
	public static void NextScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
}