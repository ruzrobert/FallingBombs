using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance { get; private set; }

    [SerializeField] private List<EnemyPawn> enemies = new List<EnemyPawn>();

	public List<EnemyPawn> Enemies => enemies;

	private void Awake()
	{
		Instance = this;
	}

	public void RegisterEnemy(EnemyPawn enemy)
	{
		enemies.Add(enemy);
	}

	public void UnRegisterEnemy(EnemyPawn enemy)
	{
		enemies.Remove(enemy);
	}
}