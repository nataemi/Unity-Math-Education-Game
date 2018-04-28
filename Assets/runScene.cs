
using UnityEngine;
using UnityEngine.SceneManagement;

public class runScene : MonoBehaviour
{
	public void NextScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
}