using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Space]
    [SerializeField] private EnemyPawnKey[] enemyVariants = new EnemyPawnKey[0];

    [Space]
    [SerializeField] private Vector2 spawnableArea = Vector2.one;

	[Space]
	[SerializeField] private float spawnPeriodicity = 0.2f;
	[SerializeField] private int maxEnemies = 5;

	private float nextEnemySpawnTime = 0f;

	private void Update()
	{
		if (IsReadyToSpawn())
		{
			Vector3 randomPosition = GetRandomPosition();

			SpawnEnemyAt(randomPosition);

			nextEnemySpawnTime = Time.time + spawnPeriodicity;
		}
	}

	private bool IsReadyToSpawn()
	{
		return EnemyManager.Instance.Enemies.Count < maxEnemies && Time.time >= nextEnemySpawnTime;
	}

	private Vector3 GetRandomPosition()
	{
		Vector3 localPosition = new Vector3();
		localPosition.x = Random.Range(-spawnableArea.x * 0.5f, spawnableArea.x * 0.5f);
		localPosition.z = Random.Range(-spawnableArea.y * 0.5f, spawnableArea.y * 0.5f);

		Vector3 worldPosition = transform.TransformPoint(localPosition);

		return worldPosition;
	}

	private void SpawnEnemyAt(Vector3 position)
	{
		EnemyPawnKey randomEnemyKey = enemyVariants.GetRandomOrDefault();

		if (randomEnemyKey)
		{
			EnemyPawn enemy = ObjectPoolingManager.Instance.EnemyPooler.GetObjectFromPool(randomEnemyKey);

			if (enemy)
			{
				EnemyManager.Instance.RegisterEnemy(enemy);

				enemy.transform.SetParent(transform);
				enemy.transform.position = new Vector3(position.x, 0f, position.z);

				enemy.gameObject.SetActive(true);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue.SetA(0.3f);
		Gizmos.DrawCube(transform.position, new Vector3(spawnableArea.x, 0.5f, spawnableArea.y));
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue.SetA(0.4f);
		Gizmos.DrawWireCube(transform.position, new Vector3(spawnableArea.x, 0.5f, spawnableArea.y));
	}
}