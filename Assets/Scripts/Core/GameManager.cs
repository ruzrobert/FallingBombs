using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

	[Space]
	[SerializeField] private bool autoStartGameOnStart = true;

	public bool IsGameStarted { get; private set; } = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		LoadLevel();

		if (autoStartGameOnStart)
		{
			StartGame();
		}
	}

	private void LoadLevel()
	{
		EventManager.Instance.Loading.OnLoadLevel.Invoke();
	}

	private void StartGame()
	{
		if (!IsGameStarted)
		{
			IsGameStarted = true;

			EventManager.Instance.GameState.OnGameStarted.Invoke();
		}
	}
}