using System;
using System.Linq;
using UnityEngine;

public class ExploadableBombPooler : KeyedObjectPooler<ExploadableBombKey, ExploadableBomb>
{
	[Serializable]
	public class KVP : KeyedPoolKVP<ExploadableBombKey, ExploadableBomb> { }

	[Space]
	[SerializeField] private KVP[] prefabs = new KVP[0];

	protected override ExploadableBomb GetPrefabByKey(ExploadableBombKey key)
	{
		return prefabs.FirstOrDefault(x => x.Key == key).Prefab;
	}
}