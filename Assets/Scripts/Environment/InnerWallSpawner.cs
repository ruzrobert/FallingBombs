using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InnerWallSpawner : MonoBehaviour
{
	[Serializable]
	public struct InnerWallRule
	{
		public Vector2 spawnableArea;
		public Transform wallPrefab;
	}

	[Space]
	[SerializeField] private InnerWallRule[] innerWallVariants = new InnerWallRule[0];

	[Space]
	[SerializeField, Min(0)] private int minWallCount = 0;
	[SerializeField, Min(0)] private int maxWallCount = 3;

	private void Awake()
	{
		EventManager.Instance.Loading.OnLoadLevel.AddListener(SpawnRandomInnerWalls);
	}

	private void SpawnRandomInnerWalls()
	{
		if (innerWallVariants.IsNullOrEmpty()) return;

		int wallCount = Random.Range(minWallCount, maxWallCount + 1);

		for (int i = 0; i < wallCount; i++)
		{
			SpawnRandomWall();
		}
	}

	private void SpawnRandomWall()
	{
		InnerWallRule wallVariant = innerWallVariants.GetRandomOrDefault();

		Transform wall = Instantiate(wallVariant.wallPrefab, transform);

		Vector3 localPosition = new Vector3();

		Vector2 area = wallVariant.spawnableArea;
		localPosition.x = Random.Range(-area.x, area.x);
		localPosition.z = Random.Range(-area.y, area.y);
		localPosition.y = 0f;

		wall.localPosition = localPosition;

		wall.localEulerAngles = wall.localEulerAngles.SetY(Random.Range(0, 2) == 0 ? 0f : 90f);
	}
}