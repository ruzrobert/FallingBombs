using System;
using System.Linq;
using UnityEngine;

public class EnemyPawnPooler : KeyedObjectPooler<EnemyPawnKey, EnemyPawn>
{
	[Serializable]
	public class KVP : KeyedPoolKVP<EnemyPawnKey, EnemyPawn> { }

	[Space]
	[SerializeField] private KVP[] prefabs = new KVP[0];

	protected override EnemyPawn GetPrefabByKey(EnemyPawnKey key)
	{
		return prefabs.FirstOrDefault(x => x.Key == key).Prefab;
	}
}