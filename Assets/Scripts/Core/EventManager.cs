using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

	public class LoadingEvents
	{
		public UnityEvent OnLoadLevel { get; } = new UnityEvent();
	}
	public class GameStateEvents
	{
		public UnityEvent OnGameStarted { get; } = new UnityEvent();
	}

	public LoadingEvents Loading { get; } = new LoadingEvents();
	public GameStateEvents GameState { get; } = new GameStateEvents();

	private void Awake()
	{
		Instance = this;
	}

	private void OnDestroy()
	{
		Instance = null;
	}
}