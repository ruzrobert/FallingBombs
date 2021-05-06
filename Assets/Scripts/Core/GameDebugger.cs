using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebugger : MonoBehaviour
{
	[Space]
	[SerializeField] private bool editorOnly = true;

	private void Update()
	{
		if (editorOnly && !Application.isEditor) return;

		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartScene();
		}
	}

	private void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}