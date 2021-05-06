using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [Space]
    [SerializeField] private List<ExploadableBombKey> bombVariants = new List<ExploadableBombKey>();

    [Space]
	[SerializeField] private Vector2 spawnableArea = Vector2.one;
	[SerializeField] private float spawnPeriodicity = 1f;

	private float nextSpawnTime = 0f;

	private bool isEnabled = false;

	private void Awake()
	{
		EventManager.Instance.GameState.OnGameStarted.AddListener(() => SetSpawnerEnabled(true));
	}

	private void Start()
	{
		// If enabled after the game is started
		if (GameManager.Instance.IsGameStarted)
		{
			SetSpawnerEnabled(true);
		}
	}

	private void Update()
	{
		if (bombVariants.Count > 0 && IsReadyToSpawn())
		{
			ExploadableBombKey randomBombKey = bombVariants.GetRandomOrDefault();

			if (randomBombKey)
			{
				ExploadableBomb bomb = PoolingManager.Instance.BombPooler.GetObjectFromPool(randomBombKey);

				if (bomb)
				{
					Vector3 randomPosition = GetRandomPosition();

					SpawnBombAt(bomb, randomPosition);
				}
			}

			nextSpawnTime = Time.time + spawnPeriodicity;
		}
	}

	private void SetSpawnerEnabled(bool enabled)
	{
		isEnabled = enabled;
	}

	private bool IsReadyToSpawn()
	{
		return isEnabled && Time.time >= nextSpawnTime;
	}

	private Vector3 GetRandomPosition()
	{
		Vector3 localPosition = new Vector3();
		localPosition.x = Random.Range(-spawnableArea.x * 0.5f, spawnableArea.x * 0.5f);
		localPosition.z = Random.Range(-spawnableArea.y * 0.5f, spawnableArea.y * 0.5f);

		Vector3 worldPosition = transform.TransformPoint(localPosition);

		return worldPosition;
	}

	private void SpawnBombAt(ExploadableBomb bomb, Vector3 position)
	{
		bomb.transform.SetParent(transform);
		bomb.transform.position = position;
		bomb.transform.rotation = Quaternion.identity;

		bomb.gameObject.SetActive(true);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red.SetA(0.2f);
		Gizmos.DrawCube(transform.position, new Vector3(spawnableArea.x, 0.5f, spawnableArea.y));
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red.SetA(0.4f);
		Gizmos.DrawWireCube(transform.position, new Vector3(spawnableArea.x, 0.5f, spawnableArea.y));
	}
}